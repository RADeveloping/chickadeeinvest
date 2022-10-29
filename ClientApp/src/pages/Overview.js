import * as React from 'react';
import {useEffect, useState} from 'react';
import useFetch from "../components/FetchData";
import {Box, Chip, Container, Grid, Grow, Stack, Typography} from "@mui/material";
import Page from "../components/Page";
import SimpleList from "../components/SimpleList";
import PageLoading from "../components/PageLoading";
import useResponsive from "../hooks/useResponsive";
import Label from "../components/Label";
import {filterProperties, filterTicket, filterUnit} from "../utils/filters";

export default function Overview() {
    const [properties, errorProperties, loadingProperties] = useFetch('/api/Properties', filterProperties);
    const [units, errorUnits, loadingUnits] = useFetch('/api/Units', filterUnit);
    const [tickets, errorTickets, loadingTickets] = useFetch('/api/Tickets', filterTicket);
    
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
            setPath(`${selectedProperty.dir}/Units/${selectedUnit.dir}`)
        } else if(selectedProperty) {
            setPath(`${selectedProperty.dir}`)
        }
    }, [selectedUnit, selectedProperty])
    
    const viewList = [
        <SimpleList leftRound items={properties} title={"Properties"} setSelect={setSelectedProperty} selected={selectedProperty}
         isDesktop={isDesktop} />,
        <SimpleList noRound skinny items={ selectedProperty ?
            units.filter((u)=> u.fid === selectedProperty.id) : []}
                    title={"Units"} setNestedSelect={setSelectedProperty} path={path} setSelect={setSelectedUnit} selected={selectedUnit}
                    isDesktop={isDesktop} />,
        <SimpleList rightRound items={ selectedUnit ? tickets.filter((t)=> t.fid === selectedUnit.id) : []}
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
                    <Grow in={!loadingData}>
                        <Box>
                        {getActiveList()}
                        </Box>
                    </Grow>
                }
            </Container>
        </Page>
      
    )
}
