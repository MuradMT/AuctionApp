import Image from "next/image";
import React from "react";
import CountdownTimer from "./CountdownTimer";
import CarImage from "./CarImage";
import { Auction } from "@/app/types";
import Link from "next/link";
import CurrentBid from "./CurrentBid";
type Props = {
  auction: Auction;
};
const AuctionCard = ({ auction }: Props) => {
  return (
    <Link href={`auctions/details/${auction.id}`} className="group">
      <div className="w-full bg-gray-200 aspect-w-16 aspect-h-10 rounded-lg overflow-hidden">
        <div>
          <CarImage imageUrl={auction.imageUrl}/>
          <div className="absolute bottom-2 left-2">
            <CountdownTimer auctionEnd={auction.auctionEnd} />
          </div>
          <div className="absolute top-2 right-2">
            <CurrentBid reservePrice={auction.reservePrice} amount={auction.currentHighBid}/>
          </div>
        </div>
      </div>
      <div className="flex justify-between items-center mt-4">
        <h3 className="text-teal-300 font-semibold">
          {auction.make} {auction.model}
        </h3>
        <p className="font-semibold text-sm text-sky-500">{auction.year}</p>
      </div>
    </Link>
  );
};

export default AuctionCard;
