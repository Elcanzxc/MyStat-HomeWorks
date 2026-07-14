export default function LastUpdated() {
  const time = new Date().toLocaleTimeString("ru-RU");
  return (
  <div>Обновление было в :{time}</div>);
}
