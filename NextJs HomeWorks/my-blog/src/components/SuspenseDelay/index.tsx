

import { delay } from '@/src/utils'


export default async function SuspenseDelay({ms}:{ms:number}) {

  await delay(ms)
  return (
       <p>Suspensed Text with delay:{ms}</p>
  )
}
