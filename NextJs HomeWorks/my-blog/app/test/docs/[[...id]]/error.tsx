'use client'

import { delay } from "@/src/utils"
import { useRouter } from "next/navigation"

export default function ErrorPage({error, reset}:{error:Error,reset: () => void}) {
  
  const router = useRouter()


  return (
    <div>
    <p>ErrorPage: {error.message}</p>
    <button onClick={reset}>Попробуй снова</button>
    </div>

  )
}
