

function DemoServerPage() {
  const renderAt = new Date().toLocaleTimeString("ru-RU");





  return(  <div>
    <h1>Server Component</h1>
    <p> Страница отрендерена в: {renderAt}</p>


  </div>)

}

export default DemoServerPage;
