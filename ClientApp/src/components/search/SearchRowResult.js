import {Grid, Grow} from "@mui/material";
import SortControl from "./SortControl";
import * as React from "react";

/**
 * Component for Search. Each SearchRowResult represents a category with its own results.
 * @param isDesktop {bool} isDesktop state.
 * @param title {string} Title of row.
 * @param orderBy {string} Order by state.
 * @param handleOrderByChange {()=>void} Handles order by state change.
 * @param properties {[object]} Properties to be mapped to order by.
 * @param order {string} Order, asc or desc state.
 * @param handleOrderChange {()=>void} Handles order state change.
 * @param viewComponent {()=>JSX.Element} Method to return the component to view the data.
 * @param data {[object]} Array of data.
 * @returns {JSX.Element}
 * @constructor
 */
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
        <Grow key={'row-result'} in={data.length > 0}>
            <Grid key={'row-result-grid'} item container spacing={1} justifyContent={isDesktop ? undefined : 'center'}
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