'use client'


import { useState } from 'react'
import { PutNotes } from './action'


export default function UpdateNoteForm({id}:{id:string}){

    const [text,setText] = useState('')
   
    console.log()
    const put = async() => {
      console.log(`UpdateNoteForm: ${text}:${id}`)
       await PutNotes({text:text,id:id})
    }


    return (
    <div>
        <input value={text} onChange={(e) => setText(e.target.value)} placeholder='vvedi text'/>
        <button onClick={put}>Izmenit</button>
    </div>
  )
}
