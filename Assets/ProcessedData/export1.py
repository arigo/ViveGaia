import sdata
import struct


f = open('Gaia-TgasSource-pos.bytes', 'wb')

for s in sdata.enum():
    if 1:# s.phot_g_mean_mag < 11.0:
        f.write(struct.pack("<ffff", s.phot_g_mean_mag,
                                     s.ra,
                                     s.dec,
                                     s.parallax))
f.close()
