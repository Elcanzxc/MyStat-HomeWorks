


import { articles } from '@/src/utils'

import TagBadge from '../TagBadge/TagBadge'
import Link from 'next/link'
import LikeArticleButton from '../LikeArticleButton'
import { ReactNode } from 'react'

export default function BlogArticles({children}:{children:ReactNode}) {
  return (
          <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
        {articles.map((item) => (
          <div 
            key={item.id} 
            className="group relative flex flex-col justify-between bg-white dark:bg-slate-800 rounded-2xl p-6 shadow-sm border border-slate-100 dark:border-slate-700 hover:shadow-md hover:border-blue-100 dark:hover:border-slate-600 transition-all duration-200"
          >
            <div>
              <div className="flex items-center justify-between mb-3">
                <span className="text-xs font-semibold text-slate-500 dark:text-slate-400">
                  #{item.id}
                </span>
                <div className="flex gap-2">
                  {item.tags.map((tag) => (
                    <TagBadge key={tag} text={tag}/>
                  ))}
                </div>
              </div>
              <h3 className="text-lg font-bold text-slate-900 dark:text-white mb-2 line-clamp-2 group-hover:text-blue-600 dark:group-hover:text-blue-400 transition-colors">
                <Link href={`blog/${item.id}`} className="focus:outline-none">
                  <span className="absolute inset-0" aria-hidden="true" />
                  {item.title}
                </Link>
              </h3>
              <p className="text-slate-600 dark:text-slate-300 text-sm line-clamp-3 mb-4">
                {item.description}
              </p>
            </div>
            
            <div className="relative z-10 pt-4 border-t border-slate-100 dark:border-slate-700 mt-auto">
              {children}
            </div>
          </div>
        ))}
      </div>
  )
}
