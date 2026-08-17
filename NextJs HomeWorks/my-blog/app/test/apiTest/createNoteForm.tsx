'use client'


import { useState } from 'react'
import { PostNotes } from './action'


export default function CreateNoteForm(){

    const [text,setText] = useState('')

    const add = async() => {

       await PostNotes(text)
    }


    return (
    <div>
        <input value={text} onChange={(e) => setText(e.target.value)} placeholder='vvedi text'/>
        <button onClick={add}>Otpravit</button>
    </div>
  )
}
