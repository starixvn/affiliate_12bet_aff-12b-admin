import React from 'react';
import { BannerDetailPopup } from '../BannerDetailPopup';
import { shallow, mount } from 'enzyme';

import { defaultPagingOption } from '../../../../../modules/constants';
import { Modal } from 'reactstrap';

describe('banner list - components - BannerDetailPopup ', () => {

    const defaultProps = {
        bannerDetailUrl: {
            src: "CategoryMedia/Casino/Baccarat/SBNG_2.gif",
            witdh: 1655,
            height: 438
        }
    };

    it('render correctly', () => {
        const result = shallow(<BannerDetailPopup {...defaultProps} />);

        expect(result).toMatchSnapshot();
    });

    it('render correctly with bannerDetailUrl is null', () => {
        const props = Object.assign({}, defaultProps, { bannerDetailUrl: null })
        const result = shallow(<BannerDetailPopup {...props} />);

        expect(result).toMatchSnapshot();
    });

    it('setBannerDetailUrl - should be called when close popup', () => {
        const props = Object.assign({}, defaultProps, { setBannerDetailUrl: jest.fn(), witdh: 900 })
        const result = shallow(<BannerDetailPopup {...props} />);

        result.find(Modal).props().toggle();

        expect(props.setBannerDetailUrl).toHaveBeenCalledWith({ src: null });
    });
})