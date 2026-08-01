"use client"

import { useState } from "react"

export default function LikeArticleButton() {
  const [count, setCount] = useState(0)
  
  return (
    <button 
      onClick={() => setCount(prev => prev + 1)}
      className="inline-flex items-center gap-2 px-3 py-1.5 text-sm font-medium text-pink-600 bg-pink-50 hover:bg-pink-100 rounded-full transition-colors w-max"
    >
      <svg className="w-4 h-4" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg">
        <path fillRule="evenodd" d="M3.172 5.172a4 4 0 015.656 0L10 6.343l1.172-1.171a4 4 0 115.656 5.656L10 17.657l-6.828-6.829a4 4 0 010-5.656z" clipRule="evenodd" />
      </svg>
      Поставить лайк: {count}
    </button>
  )
}
