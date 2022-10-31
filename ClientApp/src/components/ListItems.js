import Divider from "@mui/material/Divider";
import {ListItemButton} from "@mui/material";
import ListItemText from "@mui/material/ListItemText";
import Typography from "@mui/material/Typography";
import * as React from "react";

export default function ListItems({items}) {
    return (<>
    {items && items.length > 0 && items.map((item, index)=>
        <>
            {index !== 0 && <Divider sx={{marginLeft: 3}} key={`${item.id}-dvd1`} />}
            <ListItemButton key={`${item.id}-btn`} onClick={()=>(console.log(item))} alignItems="flex-start"
            >
                <ListItemText sx={{marginLeft:1}} key={`${item.id}-txt`}
                              primary={item.primary}
                              secondary={
                                  <>
                                      <Typography key={`${item.id}--typ`}
                                                  sx={{ display: 'inline' }}
                                                  component="span"
                                                  variant="body2"
                                                  color="text.primary"
                                      >
                                          {item.secondary}
                                      </Typography>
                                      {item.tertiary}
                                  </>
                              }
                />
            </ListItemButton>
            {index === items.length - 1 && <Divider sx={{marginLeft: 3}} key={`${item.id}-dvd`} />}
        </>)
    }</>)
}