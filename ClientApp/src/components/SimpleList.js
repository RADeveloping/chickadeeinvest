import * as React from 'react';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import Typography from '@mui/material/Typography';
import {Box, Card, Grow, ListItemButton, ListSubheader, Slide} from "@mui/material";

export default function SimpleList({items, title, setSelect, selected}) {
    return (
        <Card sx={{height:450}}>
        <List  subheader={
            <ListSubheader component="div" id="nested-list-subheader">
                {title}
            </ListSubheader>
        }
                sx={{ width: '100%', minWidth: 360, maxWidth: 200, bgcolor: 'background.paper'}}>

            {items.length > 0 && items.map((item, index)=>
                <>
                    <ListItemButton key={`${item.id}-${title}-btn`} onClick={()=>setSelect(item)} alignItems="flex-start"
                    selected={selected && (item.id === selected.id)}>
                        <ListItemText key={`${item.id}-${title}-txt`}
                            primary={item.primary}
                            secondary={
                                <React.Fragment>
                                    <Typography key={`${item.id}-${title}-typ`}
                                        sx={{ display: 'inline' }}
                                        component="span"
                                        variant="body2"
                                        color="text.primary"
                                    >
                                        {item.secondary}
                                    </Typography>
                                    {item.tertiary}
                                </React.Fragment>
                            }
                        />
                    </ListItemButton>
                    {index !== items.length - 1 && <Divider key={`${item.id}-${title}-dvd`} component="li" />}
                </>)
            }
        </List>
            {items.length === 0 &&  
                <Box sx={{
                height: '80%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                color: 'gainsboro'
            }}>{`No ${title}`}</Box>}
           
        </Card>
    );
}