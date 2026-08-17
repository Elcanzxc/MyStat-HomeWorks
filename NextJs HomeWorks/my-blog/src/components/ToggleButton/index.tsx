'use client'
export default function ToggleButton() {
    function change(){
      document.documentElement.classList.toggle('dark')
  }

  return (
     <div>
        <button onClick={change}>Изменить тему</button>
      </div>
  )
}
