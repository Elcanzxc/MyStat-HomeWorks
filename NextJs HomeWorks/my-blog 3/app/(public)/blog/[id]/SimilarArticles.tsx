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
    return <p>Похожих статей нет.</p>;
  }
    return(
     <ul>
        {similar.map((item)=>(
            <li key={item.id}>
                <Link href={`/blog/${item.id}`}>{item.title}</Link>
            </li>
        ))}
     </ul>
    )
}