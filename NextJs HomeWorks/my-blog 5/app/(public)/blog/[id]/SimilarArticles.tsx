import { delay } from "@/utils/Delay";
import Link from "next/link";

import { blog } from "@/data/blog";


export default async function SimilarArticles({ 
  currentTags, 
  currentId 
}: { 
  currentTags: string[], 
  currentId: number 
}) {

 await delay(2000);
  

 const similar = blog.filter((item) => {
    if(item.id === currentId) return false
    const commonTag = item.tags.some((tag) => currentTags.includes(tag))
    return commonTag;
 })

 
  if (similar.length === 0) {
    return <p className="text-gray-500 dark:text-gray-400">Похожих статей нет.</p>;
  }
    return(
     <ul className="list-disc pl-5">
        {similar.map((item)=>(
            <li key={item.id} className="mb-2 dark:text-gray-300">
                <Link href={`/blog/${item.id}`} className="hover:text-blue-600 dark:hover:text-blue-400 transition-colors">{item.title}</Link>
            </li>
        ))}
     </ul>
    )
}