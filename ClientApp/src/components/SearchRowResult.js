import {Grid, Grow} from "@mui/material";
import SortControl from "./SortControl";
import {propertyProperties} from "../utils/filters";
import Property from "./Property";
import * as React from "react";

export default function SearchRowResult({
                                            isDesktop,
                                            title,
                                            orderBy,
                                            handleOrderByChange,
                                            properties,
                                            order,
                                            handleOrderChange,
                                            viewComponent,
                                            data
                                        }) {
    return (
        <Grow in={data.length > 0}>
            <Grid item container spacing={1} justifyContent={isDesktop ? undefined : 'center'}
                  sx={{display: data.length === 0 ? 'none' : undefined}}>
                <Grid width={'100%'} item>
                    <SortControl title={title}
                                 orderBy={orderBy}
                                 handleOrderByChange={handleOrderByChange}
                                 properties={properties}
                                 order={order}
                                 handleOrderChange={handleOrderChange}/>
                </Grid>
                {data.map(viewComponent)}
            </Grid>
        </Grow>
    )
}