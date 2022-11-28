import { Box, IconButton, CircularProgress, useTheme } from '@mui/material';
import * as React from 'react';
interface LoadingButtonIconProps {
    loading:boolean
    text:string
    startIcon:React.ReactNode
    onClick: React.MouseEventHandler<HTMLButtonElement> | undefined
}
 
const LoadingButtonIcon: React.FunctionComponent<LoadingButtonIconProps> = ({loading,text,startIcon,onClick}) => {
    const theme=useTheme();
    return ( <Box sx={{ m: 1, position: 'relative' }}>
    <IconButton
      color="secondary"
      size="large"
      title={text}
      onClick={onClick}
      disabled={loading}
    >
      {startIcon}
    </IconButton>
    {loading && (
      <CircularProgress
        size={24}
        sx={{
          color: theme.palette.primary.main,
          position: 'absolute',
          top: '50%',
          left: '50%',
          marginTop: '-12px',
          marginLeft: '-12px',
        }}
      />
    )}
  </Box> );
}
 
export default LoadingButtonIcon;