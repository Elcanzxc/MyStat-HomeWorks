import { articles } from "@/src/utils";
import { notFound } from "next/navigation";
import React from "react";



export default async function Page({ params }: { params: Promise<{ tag: string }> }) {
  const {tag} = await params
  
    const sameArticles = articles.filter((item) =>
    item.tags.some((t) => t.toLowerCase() === tag.toLowerCase())
    );

    if (sameArticles.length === 0) {
    notFound();
    }
  return (
    <div>
       <span>{tag}</span>
        <div>
           {sameArticles.map((item) => (
             <div key={item.id}> 
                 <span>{item.id}</span>
                 <h1>{item.title}</h1>
                 <p>{item.content}</p>
             </div>
           ))}
        </div>
    
    </div>
  )

}
