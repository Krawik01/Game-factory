using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFactory.DockRush
{
    /// <summary>
    /// A deliberately self-contained first game. There is no GameFactoryCore here:
    /// this component owns this game's board, presentation and level data.
    /// </summary>
    public sealed class DockRushBootstrap : MonoBehaviour
    {
        private enum CargoColor { Coral, Mint, Gold, Violet }
        private enum ScreenState { Planning, Dispatching, Won, Lost }

        private sealed class Cargo
        {
            public CargoColor Color;
            public int Entry;
            public Cargo(CargoColor color, int entry) { Color = color; Entry = entry; }
        }

        private sealed class Level
        {
            public string Name;
            public string Hint;
            public Cargo[] Queue;
            public CargoColor[] Docks;
            public int[] Starts;
            public Level(string name, string hint, Cargo[] queue, CargoColor[] docks, int[] starts)
            {
                Name = name; Hint = hint; Queue = queue; Docks = docks; Starts = starts;
            }
        }

        private static readonly Color Ink = new Color(0.025f, 0.055f, 0.105f);
        private static readonly Color Panel = new Color(0.055f, 0.105f, 0.18f, 0.96f);
        private static readonly Color Line = new Color(0.31f, 0.48f, 0.61f, 0.75f);
        private static readonly Color Glow = new Color(0.33f, 0.91f, 0.87f);
        private static readonly Color[] CargoPalette =
        {
            new Color(1f, 0.36f, 0.34f), new Color(0.24f, 0.91f, 0.67f),
            new Color(1f, 0.76f, 0.22f), new Color(0.66f, 0.45f, 1f)
        };

        private readonly Vector2[] nodes =
        {
            new Vector2(.23f, .31f), new Vector2(.50f, .23f), new Vector2(.77f, .31f),
            new Vector2(.31f, .58f), new Vector2(.62f, .56f), new Vector2(.48f, .78f)
        };

        // 0–2 are entry hubs. Their outputs converge on 3–5; those shared hubs
        // decide the actual dock. Negative values mean dock 0–2. This keeps every
        // successful or failed route explainable on the screen, rather than hiding
        // the outcome behind a random backlog rule.
        private readonly int[,] exits =
        {
            { 3, 4, -1 }, { 4, 5, -2 }, { 5, 3, -3 },
            { -1, -2, -3 }, { -1, -2, -3 }, { -1, -2, -3 }
        };

        private readonly Level[] levels =
        {
            new Level("01 · Pierwsza zmiana", "Wybierz przełącznik, żeby zobaczyć co zmienia. Potem DISPATCH.",
                new [] { new Cargo(CargoColor.Coral, 0), new Cargo(CargoColor.Mint, 1) },
                new [] { CargoColor.Coral, CargoColor.Mint, CargoColor.Gold }, new [] { 0, 1, 0, 2, 1, 0 }),
            new Level("02 · Wspólny sygnał", "Jedna pozycja huba dotyczy kilku tras. Zobacz podgląd kolejnej skrzyni.",
                new [] { new Cargo(CargoColor.Gold, 0), new Cargo(CargoColor.Coral, 2), new Cargo(CargoColor.Mint, 1) },
                new [] { CargoColor.Mint, CargoColor.Gold, CargoColor.Coral }, new [] { 1, 2, 0, 2, 1, 1 }),
            new Level("03 · Nocny przypływ", "Nie ma zegara. Najpierw rozplanuj trzy ruchy, potem puść ładunek.",
                new [] { new Cargo(CargoColor.Violet, 1), new Cargo(CargoColor.Coral, 0), new Cargo(CargoColor.Gold, 2) },
                new [] { CargoColor.Gold, CargoColor.Violet, CargoColor.Coral }, new [] { 2, 0, 2, 1, 2, 0 }),
            new Level("04 · Krzyżowy ruch", "Pomyłka nie zapełnia paska. Konkretna skrzynia trafia do złego doku.",
                new [] { new Cargo(CargoColor.Mint, 2), new Cargo(CargoColor.Gold, 1), new Cargo(CargoColor.Violet, 0), new Cargo(CargoColor.Coral, 2) },
                new [] { CargoColor.Violet, CargoColor.Coral, CargoColor.Mint }, new [] { 0, 1, 1, 2, 0, 2 }),
            new Level("05 · Zmiana kierunku", "Przełączniki są wspólną siecią, nie trzema oddzielnymi torami.",
                new [] { new Cargo(CargoColor.Coral, 1), new Cargo(CargoColor.Violet, 2), new Cargo(CargoColor.Mint, 0), new Cargo(CargoColor.Gold, 1) },
                new [] { CargoColor.Gold, CargoColor.Coral, CargoColor.Violet }, new [] { 2, 2, 1, 0, 2, 1 }),
            new Level("06 · Ostatni prom", "Tu trzeba myśleć o następnym ładunku, zanim wypuścisz obecny.",
                new [] { new Cargo(CargoColor.Gold, 2), new Cargo(CargoColor.Mint, 0), new Cargo(CargoColor.Coral, 1), new Cargo(CargoColor.Violet, 2), new Cargo(CargoColor.Gold, 0) },
                new [] { CargoColor.Coral, CargoColor.Gold, CargoColor.Violet }, new [] { 1, 0, 2, 1, 1, 2 })
        };

        private int[] rotation = new int[6];
        private int levelIndex;
        private int cargoIndex;
        private int delivered;
        private ScreenState state;
        private string status = "Ustaw sieć dla pierwszej skrzyni.";
        private float pulse;
        private GUIStyle titleStyle;
        private GUIStyle labelStyle;
        private GUIStyle centerStyle;
        private GUIStyle buttonStyle;
        private Texture2D pixel;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void StartGame()
        {
            if (FindObjectOfType<DockRushBootstrap>() != null) return;
            var root = new GameObject("Dock Rush · Game");
            root.AddComponent<DockRushBootstrap>();
            DontDestroyOnLoad(root);
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Screen.orientation = ScreenOrientation.Portrait;
            pixel = new Texture2D(1, 1, TextureFormat.RGBA32, false);
            pixel.SetPixel(0, 0, Color.white);
            pixel.Apply();
            LoadLevel(0);
        }

        private void Update() { pulse += Time.deltaTime; }

        private void LoadLevel(int index)
        {
            levelIndex = Mathf.Clamp(index, 0, levels.Length - 1);
            Array.Copy(levels[levelIndex].Starts, rotation, rotation.Length);
            cargoIndex = 0;
            delivered = 0;
            state = ScreenState.Planning;
            status = "Sieć gotowa. Sprawdź trasę podglądu.";
        }

        private int ResolveDock(Cargo cargo)
        {
            int node = cargo.Entry == 0 ? 0 : cargo.Entry == 1 ? 1 : 2;
            for (int hop = 0; hop < 3; hop++)
            {
                int next = exits[node, rotation[node]];
                if (next < 0) return -next - 1;
                node = next;
            }
            return 0; // unreachable with the current directed graph
        }

        private List<int> PreviewPath(Cargo cargo)
        {
            var path = new List<int>();
            int node = cargo.Entry == 0 ? 0 : cargo.Entry == 1 ? 1 : 2;
            path.Add(node);
            for (int hop = 0; hop < 3; hop++)
            {
                int next = exits[node, rotation[node]];
                if (next < 0) break;
                node = next;
                path.Add(node);
            }
            return path;
        }

        private void Dispatch()
        {
            if (state != ScreenState.Planning || cargoIndex >= levels[levelIndex].Queue.Length) return;
            StartCoroutine(DispatchRoutine());
        }

        private IEnumerator DispatchRoutine()
        {
            state = ScreenState.Dispatching;
            var cargo = levels[levelIndex].Queue[cargoIndex];
            status = "Ładunek jest w drodze…";
            yield return new WaitForSeconds(.42f);
            int dock = ResolveDock(cargo);
            if (levels[levelIndex].Docks[dock] != cargo.Color)
            {
                status = "Zły dok: ten ładunek nie ma gdzie wylądować.";
                state = ScreenState.Lost;
                yield break;
            }
            delivered++;
            cargoIndex++;
            if (cargoIndex == levels[levelIndex].Queue.Length)
            {
                status = "Zmiana zamknięta. Wszystkie ładunki są na miejscu.";
                state = ScreenState.Won;
            }
            else
            {
                status = "Dobry dok. Zaplanuj następny ładunek.";
                state = ScreenState.Planning;
            }
        }

        private void Rotate(int node)
        {
            if (state != ScreenState.Planning) return;
            rotation[node] = (rotation[node] + 1) % 3;
            status = "Hub " + (node + 1) + " zmienił wspólną trasę.";
        }

        private void SetupStyles()
        {
            if (titleStyle != null) return;
            titleStyle = new GUIStyle(GUI.skin.label) { fontSize = 36, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleLeft, normal = { textColor = Color.white } };
            labelStyle = new GUIStyle(GUI.skin.label) { fontSize = 19, wordWrap = true, normal = { textColor = new Color(.79f, .88f, .95f) } };
            centerStyle = new GUIStyle(labelStyle) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
            buttonStyle = new GUIStyle(GUI.skin.button) { fontSize = 22, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter, normal = { textColor = Ink, background = pixel }, hover = { textColor = Ink, background = pixel }, active = { textColor = Ink, background = pixel } };
        }

        private void RectFill(Rect r, Color color)
        {
            var old = GUI.color; GUI.color = color; GUI.DrawTexture(r, pixel); GUI.color = old;
        }

        private void Circle(Vector2 center, float radius, Color color)
        {
            const int pieces = 28;
            var old = GUI.color; GUI.color = color;
            for (int i = 0; i < pieces; i++)
            {
                float a = i * Mathf.PI * 2f / pieces;
                float b = (i + 1) * Mathf.PI * 2f / pieces;
                Vector2 p1 = center + new Vector2(Mathf.Cos(a), Mathf.Sin(a)) * radius;
                Vector2 p2 = center + new Vector2(Mathf.Cos(b), Mathf.Sin(b)) * radius;
                DrawLine(p1, p2, 3f);
            }
            GUI.color = old;
        }

        private void DrawLine(Vector2 a, Vector2 b, float width)
        {
            var matrix = GUI.matrix;
            Vector2 d = b - a;
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            GUIUtility.RotateAroundPivot(angle, a);
            GUI.DrawTexture(new Rect(a.x, a.y - width / 2f, d.magnitude, width), pixel);
            GUI.matrix = matrix;
        }

        private void OnGUI()
        {
            SetupStyles();
            float w = Screen.width, h = Screen.height;
            Rect safe = new Rect(w * .055f, h * .035f, w * .89f, h * .93f);
            RectFill(new Rect(0, 0, w, h), Ink);
            // Subtle harbour bands make the scene feel like a place, not a debug board.
            RectFill(new Rect(0, h * .54f, w, h * .46f), new Color(.035f, .115f, .18f));
            RectFill(new Rect(0, h * .77f, w, h * .23f), new Color(.025f, .08f, .14f));

            GUI.Label(new Rect(safe.x, safe.y, safe.width, 45), "DOCK RUSH", titleStyle);
            GUI.Label(new Rect(safe.x, safe.y + 43, safe.width, 24), levels[levelIndex].Name.ToUpperInvariant(), labelStyle);

            Rect queueRect = new Rect(safe.x, safe.y + 82, safe.width, 66);
            RectFill(queueRect, Panel);
            GUI.Label(new Rect(queueRect.x + 15, queueRect.y + 7, 150, 22), "MANIFEST", labelStyle);
            for (int i = 0; i < levels[levelIndex].Queue.Length; i++)
            {
                float x = queueRect.x + 160 + i * 54;
                var c = levels[levelIndex].Queue[i];
                float alpha = i < cargoIndex ? .25f : 1f;
                Circle(new Vector2(x + 19, queueRect.y + 35), 15, new Color(CargoPalette[(int)c.Color].r, CargoPalette[(int)c.Color].g, CargoPalette[(int)c.Color].b, alpha));
                GUI.Label(new Rect(x + 10, queueRect.y + 24, 20, 24), (i + 1).ToString(), centerStyle);
            }

            Rect board = new Rect(safe.x, safe.y + 168, safe.width, safe.width * 1.04f);
            RectFill(board, new Color(.04f, .10f, .16f, .96f));
            DrawBoard(board);

            Rect statusRect = new Rect(safe.x, board.yMax + 15, safe.width, 66);
            RectFill(statusRect, Panel);
            GUI.Label(new Rect(statusRect.x + 14, statusRect.y + 9, statusRect.width - 28, statusRect.height - 12), status, labelStyle);

            Rect action = new Rect(safe.x, statusRect.yMax + 14, safe.width, 64);
            if (state == ScreenState.Planning)
            {
                var old = GUI.color; GUI.color = Glow;
                if (GUI.Button(action, "DISPATCH  →", buttonStyle)) Dispatch();
                GUI.color = old;
            }
            else if (state == ScreenState.Lost)
            {
                var old = GUI.color; GUI.color = CargoPalette[0];
                if (GUI.Button(action, "SPRÓBUJ JESZCZE RAZ", buttonStyle)) LoadLevel(levelIndex);
                GUI.color = old;
            }
            else if (state == ScreenState.Won)
            {
                var old = GUI.color; GUI.color = Glow;
                string text = levelIndex + 1 < levels.Length ? "NASTĘPNA ZMIANA  →" : "ZAGRAJ OD POCZĄTKU";
                if (GUI.Button(action, text, buttonStyle)) LoadLevel((levelIndex + 1) % levels.Length);
                GUI.color = old;
            }
            else
            {
                RectFill(action, new Color(.20f, .31f, .39f));
                GUI.Label(action, "ŁADUNEK W DRODZE", centerStyle);
            }

            GUI.Label(new Rect(safe.x, action.yMax + 16, safe.width, 32), levels[levelIndex].Hint, centerStyle);
        }

        private void DrawBoard(Rect board)
        {
            Vector2[] p = new Vector2[nodes.Length];
            for (int i = 0; i < nodes.Length; i++) p[i] = new Vector2(board.x + nodes[i].x * board.width, board.y + nodes[i].y * board.height);
            Vector2[] dockPoints =
            {
                new Vector2(board.x + board.width * .25f, board.yMax - 34),
                new Vector2(board.x + board.width * .50f, board.yMax - 34),
                new Vector2(board.x + board.width * .75f, board.yMax - 34)
            };
            var path = cargoIndex < levels[levelIndex].Queue.Length ? PreviewPath(levels[levelIndex].Queue[cargoIndex]) : null;

            for (int i = 0; i < nodes.Length; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int target = exits[i, k];
                    GUI.color = new Color(Line.r, Line.g, Line.b, target < 0 ? .30f : .58f);
                    DrawLine(p[i], target < 0 ? dockPoints[-target - 1] : p[target], 7f);
                }
            }
            if (path != null)
            {
                GUI.color = new Color(Glow.r, Glow.g, Glow.b, .72f);
                for (int i = 0; i < path.Count - 1; i++) DrawLine(p[path[i]], p[path[i + 1]], 5f);
                DrawLine(p[path[path.Count - 1]], dockPoints[ResolveDock(levels[levelIndex].Queue[cargoIndex])], 5f);
            }

            for (int dock = 0; dock < 3; dock++)
            {
                float x = board.x + (dock + 1) * board.width / 4f;
                Rect r = new Rect(x - 34, board.yMax - 54, 68, 37);
                RectFill(r, CargoPalette[(int)levels[levelIndex].Docks[dock]]);
                GUI.Label(new Rect(r.x, r.y + 8, r.width, 22), "DOK " + (dock + 1), centerStyle);
            }

            for (int i = 0; i < nodes.Length; i++)
            {
                float r = 33f + Mathf.Sin(pulse * 3f + i) * 1.2f;
                Circle(p[i], r, new Color(.13f, .23f, .31f));
                Circle(p[i], r - 5f, i == (cargoIndex < levels[levelIndex].Queue.Length ? PreviewPath(levels[levelIndex].Queue[cargoIndex])[0] : -1) ? Glow : new Color(.38f, .63f, .73f));
                GUI.Label(new Rect(p[i].x - 26, p[i].y - 14, 52, 28), (i < 3 ? "WEJ " : "WSP ") + (rotation[i] + 1), centerStyle);
                if (state == ScreenState.Planning && GUI.Button(new Rect(p[i].x - 42, p[i].y - 42, 84, 84), GUIContent.none, GUIStyle.none)) Rotate(i);
            }
        }
    }
}
