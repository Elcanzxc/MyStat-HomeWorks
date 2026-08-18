




import React from 'react'

export default function ArticleMeta({title,publishedAt,tags}:{title:string,publishedAt:string,tags:string[]}) {
  return (
    <div>
        <h2>{title}</h2>
        <p>Опубликовано: {publishedAt}</p>
        <p>Теги: {tags.join(', ')}</p>
    </div>
  )
}
