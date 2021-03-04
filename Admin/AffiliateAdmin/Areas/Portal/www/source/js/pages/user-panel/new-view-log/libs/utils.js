export const removeFirstCharacter = (value) => {
    return value.replace('@', '');
}

export const handleParseToObject = (data) => {
    const dataObject = data ? JSON.parse(data).root.row : {};

    handleContactApps(dataObject);

    return dataObject;
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