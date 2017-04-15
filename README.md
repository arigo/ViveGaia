Star renderer for HTC Vive for the Gaia project
-----------------------------------------------

A copy of the Gaia Release DR1 star positions and apparent magnitude.

https://gaia.esac.esa.int/documentation/GDR1/datamodel/

The data (ra, dec, parallax, and G-band magnitude) has already been
extracted from http://cdn.gea.esac.esa.int/Gaia/tgas_source/csv/ and a
copy is found in 'Assets/ProcessedData/'.  The scripts to redo the
extraction are also present in the same directory.

It is meant to be rendered using HTC Vive, but probably works with
other VR systems too.

After loading the Unity project, you need to manually import the
SteamVR basic library to 'Assets/Lib/SteamVR'.