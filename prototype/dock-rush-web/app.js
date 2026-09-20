const canvas = document.querySelector('#game');
const context = canvas.getContext('2d');
const levelName = document.querySelector('#level-name');
const status = document.querySelector('#status');
const queueLabel = document.querySelector('#queue');
const rotate = document.querySelector('#rotate');
const run = document.querySelector('#run');
const restart = document.querySelector('#restart');
const overlay = document.querySelector('#overlay');
const result = document.querySelector('#result');
const primaryAction = document.querySelector('#primary-action');

const palette = { Red: '#ef5a63', Blue: '#62a7f8' };
const boards = [
  { name: 'One Shared Switch', rotation: 1, actions: 1, west: 'Red', north: 'Blue', east: 'Red', south: 'Blue', hint: 'Both crates need one shared rotation. Watch the ghost routes change together.' },
  { name: 'Trade-off Check', rotation: 1, actions: 1, west: 'Blue', north: 'Red', east: 'Red', south: 'Blue', hint: 'This time, rotating would make both routes worse. Read the board, then run.' }
];

let boardIndex = 0;
let game;
let scale = 1;
let offset = { x: 0, y: 0 };

function point(x, y) { return { x: offset.x + x * scale + scale / 2, y: offset.y + y * scale + scale / 2 }; }

function resize() {
  const rect = canvas.getBoundingClientRect();
  const ratio = window.devicePixelRatio || 1;
  canvas.width = Math.round(rect.width * ratio); canvas.height = Math.round(rect.height * ratio);
  context.setTransform(ratio, 0, 0, ratio, 0, 0);
  scale = Math.min(rect.width / 5, rect.height / 5.4);
  offset = { x: (rect.width - scale * 5) / 2, y: (rect.height - scale * 5) / 2 };
  draw();
}

function start(index) {
  boardIndex = index;
  const board = boards[index];
  game = { ...board, actionsLeft: board.actions, state: 'planning' };
  levelName.textContent = `Test ${index + 1}: ${board.name}`;
  status.textContent = board.hint;
  overlay.classList.add('hidden');
  updateHud(); draw();
}

function routes() {
  return game.rotation === 0
    ? [{ from: 'west', to: 'east' }, { from: 'north', to: 'south' }]
    : [{ from: 'west', to: 'south' }, { from: 'north', to: 'east' }];
}

function updateHud() {
  const rotationText = game.rotation === 0 ? 'left → right · top → bottom' : 'left → bottom · top → right';
  queueLabel.textContent = `Switch: ${rotationText} · rotations left: ${game.actionsLeft}`;
  rotate.disabled = game.state !== 'planning' || game.actionsLeft === 0;
  run.disabled = game.state !== 'planning';
}

function rotateHub() {
  if (game.state !== 'planning' || game.actionsLeft === 0) return;
  game.rotation = game.rotation === 0 ? 1 : 0;
  game.actionsLeft -= 1;
  status.textContent = 'One rotation rewired both routes. Check who now reaches which dock.';
  updateHud(); draw();
}

function finish(won, message) {
  game.state = won ? 'won' : 'lost';
  status.textContent = message; result.textContent = message;
  primaryAction.textContent = won && boardIndex < boards.length - 1 ? 'Next test' : won ? 'Play again' : 'Try again';
  overlay.classList.remove('hidden'); updateHud(); draw();
}

function runRoutes() {
  if (game.state !== 'planning') return;
  const failed = routes().map(route => ({ crate: game[route.from], dock: game[route.to] })).find(item => item.crate !== item.dock);
  if (failed) return finish(false, `${failed.crate} crate hit a ${failed.dock} dock. You could see that before running.`);
  finish(true, 'Both routes landed. One decision solved two deliveries.');
}

function line(a, b, color) {
  context.strokeStyle = color; context.lineWidth = 9; context.lineCap = 'round'; context.setLineDash([13, 10]);
  context.beginPath(); context.moveTo(a.x, a.y); context.lineTo(b.x, b.y); context.stroke(); context.setLineDash([]);
}

function crate(position, color, label) {
  const p = point(...position);
  context.fillStyle = palette[color]; context.fillRect(p.x - scale * .2, p.y - scale * .2, scale * .4, scale * .4);
  context.fillStyle = '#14203a'; context.font = `800 ${Math.max(11, scale * .14)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle'; context.fillText(label, p.x, p.y + 1);
}

function dock(position, color, label) {
  const p = point(...position);
  context.strokeStyle = palette[color]; context.lineWidth = 7; context.strokeRect(p.x - scale * .3, p.y - scale * .3, scale * .6, scale * .6);
  context.fillStyle = palette[color]; context.font = `800 ${Math.max(11, scale * .14)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle'; context.fillText(label, p.x, p.y + 1);
}

function draw() {
  if (!game) return;
  const rect = canvas.getBoundingClientRect(); context.clearRect(0, 0, rect.width, rect.height); context.fillStyle = '#18233b'; context.fillRect(0, 0, rect.width, rect.height);
  const positions = { west: [0, 2], north: [2, 0], east: [4, 2], south: [2, 4], hub: [2, 2] };
  const hub = point(...positions.hub);
  for (const route of routes()) { const from = point(...positions[route.from]); const to = point(...positions[route.to]); line(from, hub, palette[game[route.from]]); line(hub, to, palette[game[route.from]]); }
  context.fillStyle = '#60739e'; context.fillRect(hub.x - scale * .35, hub.y - scale * .35, scale * .7, scale * .7);
  context.strokeStyle = '#d5e0ff'; context.lineWidth = 3; context.strokeRect(hub.x - scale * .35, hub.y - scale * .35, scale * .7, scale * .7);
  context.fillStyle = '#fff'; context.font = `900 ${Math.max(22, scale * .3)}px system-ui`; context.textAlign = 'center'; context.textBaseline = 'middle'; context.fillText('↻', hub.x, hub.y + 1);
  crate(positions.west, game.west, 'IN'); crate(positions.north, game.north, 'IN');
  dock(positions.east, game.east, 'DOCK'); dock(positions.south, game.south, 'DOCK');
  context.fillStyle = '#b9c7e9'; context.font = `700 ${Math.max(12, scale * .15)}px system-ui`; context.textAlign = 'center'; context.fillText('TAP HUB', hub.x, hub.y + scale * .54);
}

function tap(event) {
  const rect = canvas.getBoundingClientRect(); const hub = point(2, 2);
  if (Math.hypot(event.clientX - rect.left - hub.x, event.clientY - rect.top - hub.y) < scale * .48) rotateHub();
}

rotate.addEventListener('click', rotateHub);
run.addEventListener('click', runRoutes);
restart.addEventListener('click', () => start(boardIndex));
primaryAction.addEventListener('click', () => start(game.state === 'won' && boardIndex < boards.length - 1 ? boardIndex + 1 : game.state === 'won' ? 0 : boardIndex));
canvas.addEventListener('pointerdown', tap);
window.addEventListener('resize', resize);
start(0); resize();
