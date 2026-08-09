'use client'
import { useRouter } from 'next/navigation'

export default function ForwardButton() {
  const router = useRouter()
  return (
    <button 
      onClick={() => router.forward()}
      className="px-3 py-1.5 text-sm font-medium text-slate-700 dark:text-slate-300 bg-slate-100 dark:bg-slate-800 hover:bg-slate-200 dark:hover:bg-slate-700 rounded-md transition-colors"
    >
      Вперед
    </button>
  )
}
