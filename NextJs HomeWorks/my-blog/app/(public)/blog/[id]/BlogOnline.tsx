'use client'
import useSWR from 'swr'
import { fetcher } from './fetcher'

export default function BlogOnline() {


  const {data,error,isLoading} = useSWR('/api/onlineCount',fetcher,{refreshInterval:7000})


  return (
   <div className="flex justify-center my-4">
     {error && <p className="text-sm text-red-500 dark:text-red-400 font-medium">Не удалось загрузить данные</p>}
     {isLoading && <p className="text-sm text-slate-500 dark:text-slate-400 font-medium animate-pulse">Загружаем...</p>}
     {!isLoading && !error && (
       <div className="inline-flex items-center gap-2 bg-green-50 dark:bg-green-900/20 text-green-700 dark:text-green-400 px-3 py-1.5 rounded-full border border-green-200 dark:border-green-800/30 text-sm font-medium shadow-sm">
         <span className="relative flex h-2.5 w-2.5">
           <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-green-400 opacity-75"></span>
           <span className="relative inline-flex rounded-full h-2.5 w-2.5 bg-green-500"></span>
         </span>
         <span>Читают сейчас: {data} чел.</span>
       </div>
     )}
  </div>
  )
}
