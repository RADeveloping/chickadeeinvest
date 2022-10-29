// @mui
import PropTypes from 'prop-types';
import { alpha, styled } from '@mui/material/styles';
import {Card, CardHeader, CircularProgress, Fade, Grid, Grow, LinearProgress, Typography} from '@mui/material';
// utils
import { fShortenNumber } from '../../../utils/formatNumber';
// components
import Iconify from '../../../components/Iconify';
import ListItems from '../../../components/ListItems';
// ----------------------------------------------------------------------

const IconWrapperStyle = styled('div')(({ theme }) => ({
  margin: 'auto',
  display: 'flex',
  borderRadius: '50%',
  alignItems: 'center',
  width: theme.spacing(8),
  height: theme.spacing(8),
  justifyContent: 'center',
  marginBottom: theme.spacing(3),
}));

// ----------------------------------------------------------------------

export default function Widget({ title, total, items, icon, loading}) {
    
  return (
      <Grow in={!loading}>
    <Card
      sx={{
          height: 375,
        py: 5,
        boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
        textAlign: 'center',
        paddingTop: 0,
      }}>
      <div
          style={{
            display:'absolute',
            background: 'linear-gradient(60deg, #D9D7C3 ,#C7D9C9 )',
            height: 80,
            top: 0,
      }}
      >
        <Grid height={'100%'} alignItems={'center'} justifyContent={'space-between'} container>
          <Grid marginLeft={3} item>
              <Grid direction={'row'} gap={1.5} alignItems={'center'}  container>
                  <Grid item>
              <Iconify icon={icon} width={32} height={32} />
                  </Grid>
                  <Grid item>
              <Grid direction={'column'} alignItems={'flex-start'} container>
                  <Grid item>
                      <Typography fontSize={18} fontWeight={'bold'} lineHeight={1.25}>
                          {title}    
                      </Typography>
                  </Grid>
                  <Grid item>
                      <Typography variant="h7" sx={{color:(theme) => theme.palette['primary'].lighter}}>
                          {total} {title.toLowerCase()}
                      </Typography>
                  </Grid>
                 </Grid>
                  </Grid>
              </Grid>
          </Grid>
          <Grid  marginRight={3} item>
          </Grid>
        </Grid>
      </div>
        <ListItems items={items}/>
    </Card>
      </Grow>
  );
}
