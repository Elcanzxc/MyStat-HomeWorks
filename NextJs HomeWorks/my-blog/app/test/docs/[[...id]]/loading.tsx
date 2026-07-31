

import { delay } from '@/src/utils'
import React from 'react'

export default async function Loading() {
 await delay(2000)
  return (
    <div>Loading...</div>
  )
}
