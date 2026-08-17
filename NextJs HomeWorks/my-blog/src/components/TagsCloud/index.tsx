'use client'

import useSWR from 'swr'
import TagBadge from '../TagBadge/TagBadge'

const fetcher = (url: string) => fetch(url).then(r => r.json())

export default function TagsCloud() {
  const { data, error, isLoading } = useSWR<string[]>('/api/tags', fetcher)

  if (isLoading) return <div className="animate-pulse h-24 bg-slate-100 dark:bg-slate-800/50 rounded-xl w-full mb-8 border border-slate-200 dark:border-slate-700"></div>
  if (error || !data) return null

  return (
    <div className="mb-8 p-5 bg-white dark:bg-slate-800 border border-slate-200 dark:border-slate-700 rounded-xl shadow-sm">
      <h3 className="text-sm font-bold text-slate-500 dark:text-slate-400 uppercase tracking-wider mb-4">Навигация по тегам</h3>
      <div className="flex flex-wrap gap-2">
        {data.map(tag => (
          <div key={tag} className="hover:scale-105 transition-transform cursor-pointer">
            <TagBadge text={tag} />
          </div>
        ))}
      </div>
    </div>
  )
}
