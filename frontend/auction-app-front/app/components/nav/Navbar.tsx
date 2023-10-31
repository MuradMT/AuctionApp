import React from "react";
import {RiAuctionFill} from 'react-icons/ri';
const Navbar = () => {
  return (
    <>
      <header
        className="Navbar__Header
          sticky top-0 z-50 flex justify-between
          bg-white p-5 items-center text-cyan-300 shadow-md"
      >
        <div className="Navbar__Header__Left flex items-center gap-2 text-3xl font-semibold text-violet-600">
            <RiAuctionFill size={34}/>
            <div>Auction of Azerbaijan</div>
        </div>
        <div className="Navbar__Header__Middle">Search</div>
        <div className="Navbar__Header__Right">Login</div>
      </header>
    </>
  );
};

export default Navbar;
