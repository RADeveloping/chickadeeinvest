import {Card, CardContent, Grid, Grow, Stack, Typography} from "@mui/material";
import * as React from "react";
import {getUnitsUri} from "../utils/filters";

export default function Unit({ data, navigate }) {
    const {unitNo, propertyName, propertyId, unitId} = data;
    return (
        <Grow in={true}>
            <Grid xs={12} sm={12} md={6} l={4} xl={4} item>
                <Card sx={{height: 150}}>
                    <CardContent sx={{height: '100%'}} onClick={()=>navigate('/dashboard/' + getUnitsUri(data))}>
                        <Stack direction={'column'} justifyContent={'space-between'} alignItems={'flex-start'}
                               sx={{height: '100%'}}>
                            <Stack direction={'column'} width={'100%'}>
                                <Typography variant={'h4'} noWrap>
                                    {unitNo}
                                </Typography>
                                <Typography variant={'h6'} color="text.secondary" noWrap>
                                    {propertyName}
                                </Typography>
                            </Stack>
                        </Stack>
                    </CardContent>
                </Card>
            </Grid>
        </Grow>
    )
}