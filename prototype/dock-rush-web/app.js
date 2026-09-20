const canvas = document.querySelector('#game');
const context = canvas.getContext('2d');
const levelName = document.querySelector('#level-name');
const status = document.querySelector('#status');
const queueLabel = document.querySelector('#queue');
const overlay = document.querySelector('#overlay');
const result = document.querySelector('#result');
const primaryAction = document.querySelector('#primary-action');
const restart = document.querySelector('#restart');

const palette = { Red: '#ef5a63', Green: '#63d788', Blue: '#62a7f8', Yellow: '#f7d154' };
const directions = { North: '↑', East: '→', South: '↓', West: '←' };
const levelUrls = Array.from({ length: 6 }, (_, index) => `../../design/dock-rush/levels/level_${String(index + 1).padStart(2, '0')}.json`);

let levels = [];
let levelIndex = 0;
let game = null;
let lastTick = 0;
let scale = 1;
let offset = { x: 0, y: 0 };

function resize() {
  const rect = canvas.getBoundingClientRect();
  const ratio = window.devicePixelRatio || 1;
  canvas.width = Math.round(rect.width * ratio);
  canvas.height = Math.round(rect.height * ratio);
  context.setTransform(ratio, 0, 0, ratio, 0, 0);
  const width = rect.width;
  const height = rect.height;
  scale = Math.min(width / 5, height / 6);
  offset = { x: (width - scale * 5) / 2, y: (height - scale * 5) / 2 + scale * .5 };
  draw();
}

function cloneLevel(level) {
  return JSON.parse(JSON.stringify(level));
}

function startLevel(index) {
  levelIndex = index;
  const level = cloneLevel(levels[index]);
  const allNodes = [...level.nodes, ...level.hubs.map(hub => ({ ...hub, type: 'hub' }))];
  game = {
    level,
    nodes: new Map(allNodes.map(node => [node.id, node])),
    hubs: new Map(level.hubs.map(hub => [hub.id, hub])),
    queue: [...level.queue],
    crate: null,
    state: 'playing',
    armed: false,
    message: 'Tap a hub to rewire the belts.',
    startedAt: performance.now(),
  };
  levelName.textContent = `Level ${level.id}: ${level.name}`;
  overlay.classList.add('hidden');
  lastTick = performance.now();
  updateHud();
  draw();
}

function activeEdge(edge) {
  if (!edge.hub) return true;
  const hub = game.hubs.get(edge.hub);
  return hub && edge.activeAtRotation.includes(hub.rotation);
}

function outgoing(nodeId) {
  return game.level.edges.filter(edge => edge.from === nodeId && activeEdge(edge));
}

function finish(state, message) {
  game.state = state;
  game.message = message;
  result.textContent = message;
  primaryAction.textContent = state === 'won' && levelIndex < levels.length - 1 ? 'Next level' : state === 'won' ? 'Play again' : 'Try again';
  overlay.classList.remove('hidden');
  updateHud();
}

function tick(now) {
  if (!game || game.state !== 'playing' || !game.armed) return;
  if (now - game.startedAt < 850) return;
  if (now - lastTick < game.level.tickSeconds * 1000) return;
  lastTick = now;

  if (!game.crate) {
    if (game.queue.length === 0) {
      finish('won', 'Dock cleared. Nice recovery.');
      return;
    }
    game.crate = { ...game.queue.shift(), nodeId: 'spawn' };
    game.message = 'Crate entered the graph.';
    updateHud();
    return;
  }

  const edges = outgoing(game.crate.nodeId);
  if (edges.length !== 1) {
    finish('lost', 'Graph locked. No route remains.');
    return;
  }
  const target = game.nodes.get(edges[0].to);
  game.crate.nodeId = target.id;
  if (target.type === 'sink') {
    finish('lost', 'Dead end. The dock locked up.');
    return;
  }
  if (target.type === 'bay') {
    if (target.acceptColor === game.crate.color && target.acceptFacing === game.crate.facing) {
      game.crate = null;
      game.message = 'Perfect fit.';
      updateHud();
      return;
    }
    finish('lost', 'Wrong bay orientation. The graph locked.');
  }
}

function point(position) {
  return { x: offset.x + position[0] * scale + scale / 2, y: offset.y + position[1] * scale + scale / 2 };
}

function drawArrow(x1, y1, x2, y2, color, active) {
  context.strokeStyle = active ? color : '#3c4a68';
  context.lineWidth = active ? 8 : 4;
  context.setLineDash(active ? [] : [7, 8]);
  context.beginPath(); context.moveTo(x1, y1); context.lineTo(x2, y2); context.stroke();
  context.setLineDash([]);
  if (!active) return;
  const angle = Math.atan2(y2 - y1, x2 - x1);
  context.fillStyle = color;
  context.save(); context.translate(x2, y2); context.rotate(angle); context.beginPath(); context.moveTo(0, 0); context.lineTo(-16, -9); context.lineTo(-16, 9); context.closePath(); context.fill(); context.restore();
}

function drawNode(node) {
  const { x, y } = point(node.position);
  if (node.type === 'hub') {
    context.fillStyle = '#60739e';
    context.fillRect(x - scale * .32, y - scale * .32, scale * .64, scale * .64);
    context.strokeStyle = '#c9d7ff'; context.lineWidth = 3; context.strokeRect(x - scale * .32, y - scale * .32, scale * .64, scale * .64);
    context.fillStyle = '#fff'; context.font = `800 ${Math.max(17, scale * .28)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle';
    context.fillText('↻', x, y + 1);
    return;
  }
  if (node.type === 'bay') {
    context.strokeStyle = palette[node.acceptColor]; context.lineWidth = 7;
    context.strokeRect(x - scale * .3, y - scale * .3, scale * .6, scale * .6);
    context.fillStyle = '#111a2d'; context.fillRect(x - scale * .22, y - scale * .22, scale * .44, scale * .44);
    context.fillStyle = palette[node.acceptColor]; context.font = `800 ${Math.max(14, scale * .22)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle';
    context.fillText(directions[node.acceptFacing], x, y);
    return;
  }
  context.fillStyle = node.type === 'sink' ? '#5a3045' : '#34415e';
  context.beginPath(); context.arc(x, y, scale * .22, 0, Math.PI * 2); context.fill();
  context.fillStyle = '#b9c7e9'; context.font = `700 ${Math.max(10, scale * .14)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle';
  context.fillText(node.type === 'spawn' ? 'IN' : node.type === 'sink' ? '×' : '•', x, y);
}

function drawCrate() {
  if (!game?.crate) return;
  const node = game.nodes.get(game.crate.nodeId);
  const { x, y } = point(node.position);
  context.fillStyle = palette[game.crate.color];
  context.fillRect(x - scale * .18, y - scale * .18, scale * .36, scale * .36);
  context.fillStyle = '#17213a'; context.font = `900 ${Math.max(16, scale * .25)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle';
  context.fillText(directions[game.crate.facing], x, y + 1);
}

function draw() {
  const width = canvas.getBoundingClientRect().width;
  const height = canvas.getBoundingClientRect().height;
  context.clearRect(0, 0, width, height);
  if (!game) return;
  context.fillStyle = '#18233b'; context.fillRect(0, 0, width, height);
  for (const edge of game.level.edges) {
    const from = game.nodes.get(edge.from); const to = game.nodes.get(edge.to);
    const a = point(from.position); const b = point(to.position);
    drawArrow(a.x, a.y, b.x, b.y, '#9db5e5', activeEdge(edge));
  }
  for (const node of game.nodes.values()) drawNode(node);
  drawCrate();
}

function updateHud() {
  if (!game) return;
  status.textContent = game.message;
  const colors = [game.crate, ...game.queue].filter(Boolean).map(crate => `<span style="color:${palette[crate.color]}">■</span>`).join(' ');
  queueLabel.innerHTML = colors ? `Queue ${colors}` : 'Queue clear';
}

function tap(event) {
  if (!game || game.state !== 'playing') return;
  const rect = canvas.getBoundingClientRect();
  const x = event.clientX - rect.left; const y = event.clientY - rect.top;
  for (const hub of game.hubs.values()) {
    const at = point(hub.position);
    if (Math.hypot(x - at.x, y - at.y) < scale * .43) {
      hub.rotation = (hub.rotation + 1) % 4;
      game.armed = true;
      game.startedAt = performance.now();
      lastTick = game.startedAt;
      game.message = `Hub rotated. Every attached route changed.`;
      updateHud(); draw();
      return;
    }
  }
}

primaryAction.addEventListener('click', () => startLevel(game.state === 'won' && levelIndex < levels.length - 1 ? levelIndex + 1 : game.state === 'won' ? 0 : levelIndex));
restart.addEventListener('click', () => startLevel(levelIndex));
canvas.addEventListener('pointerdown', tap);
window.addEventListener('resize', resize);

async function boot() {
  levels = await Promise.all(levelUrls.map(url => fetch(url).then(response => response.json())));
  startLevel(0);
  resize();
  requestAnimationFrame(function frame(now) { tick(now); draw(); requestAnimationFrame(frame); });
}

boot().catch(error => { status.textContent = `Could not load levels: ${error.message}`; });
