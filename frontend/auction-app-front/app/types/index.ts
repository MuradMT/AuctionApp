//There is one major difference 
//between interfaces and type aliases in typescript:
//Interfaces are open,it means you can modify it after decleration
//Type assertions are close,you can declare it once a time
//it resembles to readonly and const in c#

export type PagedResult<T>={
     results:T[],
     pageCount:number,
     totalCount:number
}
export type Auction= {
    reservePrice: number
    seller: string
    winner?: string
    soldAmount: number
    currentHighBid: number
    createdAt: string
    updatedAt: string
    auctionEnd: string
    status: string
    make: string
    model: string
    year: number
    color: string
    mileage: number
    imageUrl: string
    id: string
  }