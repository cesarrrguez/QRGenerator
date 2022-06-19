btn.addEventListener('click', () => {
  let url = 'https://localhost:7079/qr';
  url += '?text=' + textQR.value;

  fetch(url)
    .then((res) => res.text())
    .then((text) => (qr.src = 'data:image/png;base64,' + text));
});
