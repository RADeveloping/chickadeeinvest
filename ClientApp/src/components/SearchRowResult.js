import {Grid} from "@mui/material";
import SortControl from "./SortControl";
import {propertyProperties} from "../utils/filters";
import Property from "./Property";
import * as React from "react";

export default function SearchRowResult({
                                            isDesktop,
                                            title,
                                            loadingSearch,
                                            orderBy,
                                            handleOrderByChange,
                                            properties,
                                            order,
                                            handleOrderChange,
                                            viewComponent,
                                            data
                                        }) {
    return (
        <Grid item container spacing={1} justifyContent={isDesktop ? undefined : 'center'}>
            <Grid width={'100%'} item>
                <SortControl title={title} loadingSearch={loadingSearch}
                             orderBy={orderBy}
                             handleOrderByChange={handleOrderByChange}
                             properties={properties}
                             order={order}
                             handleOrderChange={handleOrderChange}/>
            </Grid>
            {data.map(viewComponent)}
        </Grid>
    )
}