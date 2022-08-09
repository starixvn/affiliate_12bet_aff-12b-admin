import { globalResource } from '../../../../modules/globalResource';

export { globalResource }

export const allRates = "0";

export const minDate = new Date(2022, 8, 1);

export const resource = {
    Index: globalResource.Index,
    Affiliate: 'Affiliate',
    Rate: 'Rate',
    From: globalResource.From,
    To: globalResource.To,
    Search: globalResource.Search,
    SentDate: 'Sent Date',
    Message: 'Message',
}

export const rateList = [
    {
        RateId: 5,
        RateName: 'Very Pleased',
        Checked: true
    },
    {
        RateId: 4,
        RateName: 'Satisfied',
        Checked: true
    }, {
        RateId: 3,
        RateName: 'Neutral',
        Checked: true
    },
    {
        RateId: 2,
        RateName: 'Unsatisfied',
        Checked: true
    },
    {
        RateId: 1,
        RateName: 'Dissatisfaction',
        Checked: true
    },
    {
        RateId: 99,
        RateName: 'None',
        Checked: true
    }
]