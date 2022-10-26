import * as React from 'react';
import useFetch from "../components/FetchData";
import {Box, CircularProgress, Container, Grow, Stack, Typography} from "@mui/material";
import Page from "../components/Page";
import SimpleList from "../components/SimpleList";
import {useEffect, useState} from "react";
import PageLoading from "../components/PageLoading";

export default function Overview() {
    const filterProperties = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.propertyId,
                primary: d.address});
        })
        return simpleData;
    }
    const filterUnit = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.unitId,
                fid: d.propertyId,
                primary: `${d.unitNo}`});
        })
        return simpleData;
    }
    const filterTicket = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.ticketId,
                fid: d.unitId,
                primary: `#${d.ticketId} ${d.problem}`,
                secondary: d.description});
        })
        return simpleData;
    }
    const [properties, errorProperties, loadingProperties] = useFetch('/api/Property', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch('/api/Unit', filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch('/api/Ticket', filterTicket);
    
    const [selectedProperty, setSelectedProperty] = useState(null);
    const [selectedUnit, setSelectedUnit] = useState(null);
    const [selectedTicket, setSelectedTicket] = useState(null);
    const loadingData = loadingProperties && loadingUnits && loadingTickets;
    
    useEffect(()=> {
        setSelectedUnit(null);
    }, [selectedProperty])

    useEffect(()=> {
        setSelectedTicket(null);
    }, [selectedUnit])
    
    
    const title = "Overview"
    
    return(
        
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                </Stack>
                <PageLoading loadingData={loadingData} />
                <Grow in={!loadingProperties && !loadingUnits && !loadingTickets}>
                <Stack direction="row">
                    
                    <SimpleList items={properties} title={"Properties"} setSelect={setSelectedProperty} selected={selectedProperty}/>

                    <SimpleList items={ selectedProperty ?
                        units.filter((u)=> u.fid === selectedProperty.id) : []} 
                                title={"Units"}  setSelect={setSelectedUnit} selected={selectedUnit}/>

             
                       <SimpleList items={ selectedUnit ? tickets.filter((t)=> t.fid === selectedUnit.id) : []} 
                                   title={"Tickets"}  setSelect={setSelectedTicket} selected={selectedTicket}/>
                  
              

                </Stack>
                </Grow>
            </Container>
        </Page>
      
    )
}
