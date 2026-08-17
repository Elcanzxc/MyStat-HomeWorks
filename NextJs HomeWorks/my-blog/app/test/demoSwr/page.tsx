"use client";

import useSWR from "swr";
import { fetcher } from "./fetcher";

export default function Page() {
  const { data, error, isLoading } = useSWR("/demoSwr/api", fetcher, {
    refreshInterval: 10,
  });

  if(error) return <p>Ne udalos</p>
  if(isLoading) return <p>Loading...</p> 
  return (<div>
     <p>Laykov:{data.count}</p>
  </div>)
}
