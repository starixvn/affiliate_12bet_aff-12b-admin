export const removeFirstCharacter = (value) => {
    return value.replace('@', '');
}

export const handleParseToObject = (obj) => {
    const dataObject = obj ? JSON.parse(obj).root.row : {};

    handleContactApps(dataObject);

    return cleanBlankAttributes(dataObject);
}

export const mergeToNonDuplicateArray = (obj1, obj2) => {
    const arr1 = Object.keys(obj1);
    const arr2 = Object.keys(obj2);

    return [...new Set([ ...arr1, ...arr2])];
}

const cleanBlankAttributes = (obj) => {
    for (let propName in obj) {
        if (!obj[propName]) {
            delete obj[propName];
        }
    }

    return obj
}

const handleContactApps = (objectData) => {
    const contactApps = objectData['@ContactApps'];

    if (contactApps) {
        const contactAppList = JSON.parse(contactApps);

        delete objectData['@ContactApps'];

        Array.isArray(contactAppList) && contactAppList.map(item => {
            objectData[`@${item.Name}`] = item.Value;
        });
    }
}