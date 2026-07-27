"use client";

import {ReactNode} from "react";
import {useRouter} from "next/navigation";


export default function LayoutPage({
children
}:{
children:ReactNode
}){


const router=useRouter();



return (

<div className="max-w-[900px] mx-auto p-5">

<button
className="bg-blue-600 text-white border-none px-5 py-3 rounded-[10px] text-base mb-5 transition-all duration-200 hover:bg-blue-700 hover:-translate-y-0.5 dark:bg-blue-500 dark:hover:bg-blue-600"
onClick={()=>router.back()}
>

← Вернуться назад

</button>


{children}


</div>

)

}