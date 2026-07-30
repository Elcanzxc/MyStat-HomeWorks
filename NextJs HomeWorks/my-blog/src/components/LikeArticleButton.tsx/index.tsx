"use client"

import { useState } from "react"

export default function LikeArticleButton() {
  const [count,setCount] = useState(0)
  return (
    <button onClick={() => setCount(prev => prev + 1)}>Поставить лайк:{count}</button>
  )
}
