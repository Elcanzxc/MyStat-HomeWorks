'use client'

import { useActionState, useEffect } from "react"
import { FormState, submitComment } from "./actions"

type Props = {
    onSuccess: (comment: {
        author: string
        text: string
    }) => void
}



const initialState :FormState = {}

export default function Comments() {

    const [state,formAction,isPending] = useActionState(submitComment,initialState)


  return (
    <div className="bg-slate-50 dark:bg-slate-800/30 border border-slate-200 dark:border-slate-700 p-6 rounded-xl">
        <form action={formAction} className="flex flex-col gap-4">
            <div>
                <input 
                  type="text" 
                  name='author' 
                  placeholder='Ваше имя' 
                  className="w-full px-4 py-2.5 bg-white dark:bg-slate-900 border border-slate-300 dark:border-slate-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 dark:text-white transition-colors"
                />
                {state.errors?.author && <p className="text-red-500 dark:text-red-400 text-sm mt-1.5">{state.errors.author}</p>}
            </div>
            <div>
                <textarea 
                  name='text' 
                  placeholder='Написать комментарий...' 
                  rows={3}
                  className="w-full px-4 py-2.5 bg-white dark:bg-slate-900 border border-slate-300 dark:border-slate-600 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 dark:text-white transition-colors resize-none"
                />
                {state.errors?.text && <p className="text-red-500 dark:text-red-400 text-sm mt-1.5">{state.errors.text}</p>}
            </div>
            <div className="flex items-center justify-between mt-2">
              <button 
                type='submit' 
                disabled={isPending}
                className="px-6 py-2 bg-blue-600 hover:bg-blue-700 disabled:bg-blue-400 dark:disabled:bg-blue-800 text-white font-medium rounded-lg transition-colors shadow-sm"
              >
                  {isPending ? 'Отправка...' : 'Отправить'}
              </button>
              {state.success && <p className="text-green-600 dark:text-green-400 text-sm font-medium">Комментарий добавлен!</p>}
            </div>
        </form>
    </div>
  )
}






