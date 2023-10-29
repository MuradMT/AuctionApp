import React from 'react'
//Next server side rendering gives us opportunity not to 
//demonstrate any api call on client side
//rather than we are showing the pure data
//it is just amazing feature
async function getData(){
    const res=await fetch("http://localhost:6001/search");
    if(!res.ok) throw new Error("Failed to fetch data");
    return res.json();
}

 const  Listings = async () => {
    const data=await getData();
  return (
    <div>Listings</div>
  )
}

export default Listings