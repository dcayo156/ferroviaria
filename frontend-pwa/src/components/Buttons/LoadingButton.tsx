import { Box, Button, CircularProgress, useTheme } from '@mui/material';
import * as React from 'react';
interface LoadingButtonProps {
    loading:boolean
    text:string
    startIcon:React.ReactNode
    onClick: React.MouseEventHandler<HTMLButtonElement> | undefined
}
 
const LoadingButton: React.FunctionComponent<LoadingButtonProps> = ({loading,text,startIcon,onClick}) => {
    const theme=useTheme();
    return ( <Box sx={{ m: 1, position: 'relative' }}>
    <Button
      variant="contained"
      onClick={onClick}
      disabled={loading}
      startIcon={startIcon}
    >
      {text}
    </Button>
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
 
export default LoadingButton;