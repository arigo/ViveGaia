import os


class try_float_prop(object):
    def __init__(self, index):
        self.index = index
    def __get__(self, star, cls=None):
        s = star._line.split(',')[self.index]
        if '.' in s:
            return float(s)
        return s


class Star(object):
    def __init__(self, line):
        self._line = line


def enum(start=0):
    for num in range(start, 16):
        fn = 'raw/TgasSource_000-000-%03d.csv' % num
        print fn
        f = os.popen('zcat %s' % fn)
        header = f.readline()
        for i, name in enumerate(header.split(',')):
            setattr(Star, name, try_float_prop(i))
        for line in f.readlines():
            yield Star(line)
        f.close()

def load(start=0):
    return list(enum(start=start))

if __name__ == '__main__':
    stars = load(start=15)
