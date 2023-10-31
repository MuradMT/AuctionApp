"use client"
import React, { useEffect, useState } from "react";
import AuctionCard from "./AuctionCard";
import { Auction} from "@/app/types";
import AppPagination from "../shared/AppPagination";
import { getData } from "@/app/server/auctionActions";
import Filters from "./Filters";

const Listings =  () => {
  const[auctions,setAuctions]=useState<Auction[]>([]);
  const[pageCount,setPageCount]=useState(0);
  const[pageNumber,setPageNumber]=useState(1);
  const[pageSize,setPageSize]=useState(4);
  useEffect(()=>{
    getData(pageNumber,pageSize).then(data=>{
      setAuctions(data.results),
      setPageCount(data.pageCount)
    })
  },[pageNumber,pageSize])
  if(auctions.length===0) return <h3 className="text-sky-500 font-semibold">Loading...</h3>
  return (
    <>
     <Filters pageSize={pageSize} setPageSize={setPageSize}/>
      <div className="grid grid-cols-4 gap-6">
        {
          auctions.map((auction) => (
            <AuctionCard auction={auction} key={auction.id} />
          ))}
      </div>
      <div className="flex justify-center mt-4">
        <AppPagination pageChanged={setPageNumber} currentPage={pageNumber} pageCount={pageCount}/>
      </div>
    </>
  );
};

export default Listings;
