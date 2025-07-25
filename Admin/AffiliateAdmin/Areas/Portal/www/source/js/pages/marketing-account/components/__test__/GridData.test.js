import React from 'react';
import { GridData } from '../GridData';
import { shallow } from 'enzyme';

// Mock the custom components
jest.mock('../../../common/components/NotificationPopup', () => ({
    NotificationPopup: ({ message, onClose }) => (
        <div data-testid="notification-popup" onClick={onClose}>
            {message}
        </div>
    )
}));

describe('marketing-account - components - GridData', () => {
    const defaultProps = {
        params: {
            Status: 'Approved',
            PageIndex: 1,
            PageSize: 20
        },
        languageCodeList: {
            'en': 'English',
            'zh-CN': 'Chinese'
        },
        targetCountryCodeList: {
            'US': 'United States',
            'CN': 'China'
        },
        pagingOption: {
            PageIndex: 1,
            PageSize: 20,
            TotalItem: 100
        },
        canAddAffMktAccount: true,
        toggleLoader: jest.fn(),
        fetchMarketingAccounts: jest.fn().mockResolvedValue({
            MainData: [],
            TotalItem: 0
        }),
        setPagingOption: jest.fn(),
        handleAddNewAccount: jest.fn()
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('should render correctly', () => {
        const wrapper = shallow(<GridData {...defaultProps} />);
        expect(wrapper).toMatchSnapshot();
    });

    it('should render when canAddAffMktAccount is false', () => {
        const props = { ...defaultProps, canAddAffMktAccount: false };
        const wrapper = shallow(<GridData {...props} />);

        expect(wrapper).toMatchSnapshot();
    });

    it('should not crash when params is null', () => {
        const props = { ...defaultProps, params: null };
        
        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should not crash with empty parameters', () => {
        const props = { ...defaultProps, params: {} };
        
        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should not crash with missing language and country mappings', () => {
        const props = {
            ...defaultProps,
            languageCodeList: {},
            targetCountryCodeList: {}
        };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should not crash with empty pagingOption', () => {
        const props = {
            ...defaultProps,
            pagingOption: {}
        };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should not crash with minimal props', () => {
        const minimalProps = {
            params: null,
            languageCodeList: {},
            targetCountryCodeList: {},
            pagingOption: {},
            canAddAffMktAccount: false,
            toggleLoader: jest.fn(),
            fetchMarketingAccounts: jest.fn(),
            setPagingOption: jest.fn()
        };

        expect(() => {
            shallow(<GridData {...minimalProps} />);
        }).not.toThrow();
    });

    it('should handle different language code lists without crashing', () => {
        const props = {
            ...defaultProps,
            languageCodeList: {
                'fr': 'French',
                'de': 'German'
            }
        };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should handle different target country code lists without crashing', () => {
        const props = {
            ...defaultProps,
            targetCountryCodeList: {
                'FR': 'France',
                'DE': 'Germany'
            }
        };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should handle different paging options without crashing', () => {
        const props = {
            ...defaultProps,
            pagingOption: {
                PageIndex: 2,
                PageSize: 50,
                TotalItem: 200
            }
        };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should accept all required props without crashing', () => {
        expect(() => {
            shallow(<GridData {...defaultProps} />);
        }).not.toThrow();
    });

    it('should handle props with handleAddNewAccount', () => {
        const handleAddNewAccount = jest.fn();
        const props = { ...defaultProps, handleAddNewAccount };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should accept boolean canAddAffMktAccount values', () => {
        // Test true
        expect(() => {
            shallow(<GridData {...defaultProps} />);
        }).not.toThrow();

        // Test false
        const falseProps = { ...defaultProps, canAddAffMktAccount: false };
        expect(() => {
            shallow(<GridData {...falseProps} />);
        }).not.toThrow();
    });

    it('should render with different fetchMarketingAccounts implementations', () => {
        const mockFetch = jest.fn().mockResolvedValue({
            MainData: [],
            TotalItem: 0
        });
        const props = { ...defaultProps, fetchMarketingAccounts: mockFetch };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should render with fetchMarketingAccounts that returns null', () => {
        const mockFetch = jest.fn().mockResolvedValue(null);
        const props = { ...defaultProps, fetchMarketingAccounts: mockFetch };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should render with different toggleLoader implementations', () => {
        const mockToggleLoader = jest.fn();
        const props = { ...defaultProps, toggleLoader: mockToggleLoader };

        expect(() => {
            shallow(<GridData {...props} />);
        }).not.toThrow();
    });

    it('should render component structure consistently', () => {
        const wrapper = shallow(<GridData {...defaultProps} />);
        
        // Component should render something
        expect(wrapper).toBeDefined();
        expect(wrapper.length).toBe(1);
    });

    it('should handle props updates without crashing', () => {
        const wrapper = shallow(<GridData {...defaultProps} />);

        expect(() => {
            const newParams = { ...defaultProps.params, Status: 'Suspended' };
            wrapper.setProps({ params: newParams });
        }).not.toThrow();
    });

    it('should maintain stability with complex prop combinations', () => {
        const complexProps = {
            params: {
                Status: 'Approved',
                PageIndex: 5,
                PageSize: 100,
                Language: 'fr',
                TargetCountryCode: 'FR'
            },
            languageCodeList: {
                'en': 'English',
                'fr': 'French',
                'de': 'German',
                'es': 'Spanish'
            },
            targetCountryCodeList: {
                'US': 'United States',
                'FR': 'France',
                'DE': 'Germany',
                'ES': 'Spain'
            },
            pagingOption: {
                PageIndex: 5,
                PageSize: 100,
                TotalItem: 1000
            },
            canAddAffMktAccount: true,
            toggleLoader: jest.fn(),
            fetchMarketingAccounts: jest.fn().mockResolvedValue({
                MainData: [],
                TotalItem: 1000
            }),
            setPagingOption: jest.fn(),
            handleAddNewAccount: jest.fn()
        };

        expect(() => {
            shallow(<GridData {...complexProps} />);
        }).not.toThrow();
    });
}); 