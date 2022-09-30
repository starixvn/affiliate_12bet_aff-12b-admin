import { maxWidth, deviceTypes, minDate } from '../../../../modules/constants';
import { headerText } from '../../referrers/libs/constants';
import { globalResource } from '../../../../modules/globalResource';

export { globalResource, deviceTypes, maxWidth, minDate }

export const headerTitle = {
    Page: 'Page',
    GroupBy: 'Group By',
    Language: 'Language',
    ...headerText
}

export const groupByCountry = 'Country';

export const groupByList = [groupByCountry, 'Language']
