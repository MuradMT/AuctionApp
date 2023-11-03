"use server";
//Next server side rendering gives us opportunity not to
//demonstrate any api call on client side
//rather than we are showing the pure data
//it is just amazing feature
import { Auction, PagedResult } from "@/app/types";
import { getTokenWorkaround } from "./authAuctions";

export async function getData(query: string): Promise<PagedResult<Auction>> {
  const res = await fetch(`http://localhost:6001/search${query}`);
  if (!res.ok) throw new Error("Failed to fetch data");
  return res.json();
}
export async function UpdateAuctionTest() {
     const data={
      mileage:Math.floor(Math.random()*100000)+1
     }
     const token=await getTokenWorkaround();
     const res=await fetch('http://localhost:6001/auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c',{
      method:'PUT',
      headers:{
        'Content-type':'application/json',
        'Authorization':'Bearer ' + token?.access_token
      },
      body:JSON.stringify(data)
     })
     if(!res.ok) return {status:res.status,message:res.statusText}
     return res.statusText
}
