import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Container, Grow, Stack, Typography} from "@mui/material";
import Page from "../components/Page";
import SimpleList from "../components/SimpleList";
import PageLoading from "../components/PageLoading";
import useResponsive from "../hooks/useResponsive";

export default function Overview() {
    const getFilter = (idKey, fidKey, primaryKey, secondaryKey, tertiaryKey) => {
        return (data) => {
            let simpleData = []
            data.forEach((d) => {
                simpleData.push({
                    id: d[idKey],
                    primary: d[primaryKey],
                    secondary: d[secondaryKey],
                    tertiary: d[tertiaryKey],
                    fid: d[fidKey],
                    data: d
                });
            })
            return simpleData;
        };
    }

    const filterProperties = getFilter("propertyId", null, "address")
    const filterUnit = getFilter("unitId", "propertyId", "unitNo")

    const filterTicket = (data) => {
        let simpleData = []
        data.forEach((d)=> {
            simpleData.push({
                id: d.ticketId,
                fid: d.unitId,
                primary: `#${d.ticketId} ${d.problem}`,
                secondary: d.description,
                data: d});
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
    
    const [path, setPath] = useState('');

    const isDesktop = useResponsive('up', 'lg');

    const title = "Overview"
    
    useEffect(()=> {
        setSelectedUnit(null);
    }, [selectedProperty])

    useEffect(()=> {
        setSelectedTicket(null);
    }, [selectedUnit])
    
    useEffect(() => {
        if (selectedUnit) {
            setPath(`${selectedProperty.primary}/Units/${selectedUnit.primary}`)
        } else if(selectedProperty) {
            setPath(`${selectedProperty.primary}`)
        }
    }, [selectedUnit, selectedProperty])
    
    const viewList = [
        <SimpleList items={properties} title={"Properties"} setSelect={setSelectedProperty} selected={selectedProperty}
         isDesktop={isDesktop} />,
        <SimpleList skinny items={ selectedProperty ?
            units.filter((u)=> u.fid === selectedProperty.id) : []}
                    title={"Units"} setNestedSelect={setSelectedProperty} path={path} setSelect={setSelectedUnit} selected={selectedUnit}
                    isDesktop={isDesktop} />,
        <SimpleList items={ selectedUnit ? tickets.filter((t)=> t.fid === selectedUnit.id) : []}
                    title={"Tickets"} setNestedSelect={setSelectedUnit} path={path} setSelect={setSelectedTicket} selected={selectedTicket}
                    isDesktop={isDesktop} />
    ]
    
    function getActiveList() {
        if (selectedProperty && selectedUnit) {
            return viewList[2]
        } else if (selectedProperty) {
            return viewList[1]
        } else {
            return viewList[0]
        }
    }
    
    return(
        
        <Page title={title}>
            <Container>
                <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                    <Typography variant="h4" gutterBottom>
                        {title}
                    </Typography>
                </Stack>
                <PageLoading loadingData={loadingData} />
                {isDesktop &&
                <Grow in={!loadingData}>
                    <Stack direction="row">
                        {viewList}
                    </Stack>
                </Grow>
                }
                {!isDesktop &&
                    getActiveList()
                }
            </Container>
        </Page>
      
    )
}
