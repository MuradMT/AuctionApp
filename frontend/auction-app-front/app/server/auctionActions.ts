"use server"
//Next server side rendering gives us opportunity not to
//demonstrate any api call on client side
//rather than we are showing the pure data
//it is just amazing feature
import { Auction, PagedResult } from "@/app/types";

export async function getData(pageNumber:number=1): Promise<PagedResult<Auction>> {
    const res = await fetch(`http://localhost:6001/search?pageSize=4&pageNumber=${pageNumber}`);
    if (!res.ok) throw new Error("Failed to fetch data");
    return res.json();
  }