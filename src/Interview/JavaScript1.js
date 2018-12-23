var customSortString = function (S, T) {
    if (!S)
        return T;
    if (!T)
        return S;

    let leftover = '';

    let sortMap = new Map();

    S.split('').forEach(item => {
        if (!sortMap.has(item)) {
            sortMap.set(item, []);
        }
    });

    T.split('').forEach(item => {
        if (sortMap.has(item)) {
            sortMap.get(item).push(item);
        }
        else {
            leftover += item;
        }
    });

    let ret = '';

    for (const [key, value] of sortMap) {
        value.forEach(char => {
            ret += (char);
        });
    }

    return ret + leftover;

};