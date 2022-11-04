// import { useState } from "react";

import useFetch from "../components/FetchData";

export default function TicketSubmission() {
  // const [currentUnit, errorUnit, loadingUnit] = useFetch("/api/properties/2dd36174-1574-4c4d-a3bc-f2b40367729e/units/d79a4b8d-bf26-48e4-a028-bee969fd3f2e");
  // const [currentUser, errorUser, loadingUser] = useFetch("/api/Account");
  // console.log(currentUnit);
  // console.log(currentUser);
  let handleSubmit = async (e) => {
    e.preventDefault();
    try {
      let res = await fetch("/api/tickets/post", {
        method: "POST",
        headers: { 'Accept': 'application/json', 'Content-Type': 'application/json' },
        body: JSON.stringify({
          ticketId: 77,
          problem: "problem",
          description: "description",
          createdOn: new Date(),
          estimatedDate: new Date(),
          status: 0,
          severity: 2,
          closedDate: null,
          unitId: "02d6539f-48bc-42e4-8113-c5d1c9274782",
          // unit: currentUnit,
          createdById: "2c0eb456-4496-4fd0-bce1-4084a5427c90",
          // createdBy: currentUser
        })})
      let resJson = await res.json();
      // console.log(resJson);
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="App">
      <form onSubmit={handleSubmit}>

        <button type="submit">Create</button>

        {/* <div className="message">{message ? <p>{message}</p> : null}</div> */}
      </form>
    </div>
  );
}