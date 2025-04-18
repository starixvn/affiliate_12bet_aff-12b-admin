import React from 'react';
import { DomainPopup } from '../DomainPopup';
import { shallow } from 'enzyme';

describe('DomainPopup Component', () => {
    const emptyDomain = {
        domainId: 0,
        domainName: '',
        blockMarkets: [],
        isSSL: true
    };

    const selectedDomain = {
        domainId: 123,
        domainName: 'abc.com',
        blockMarkets: ['en-US'],
        isSSL: true
    };

    const marketListByDomains = [
        {
            MarketId: 'en-US',
            MarketName: 'English',
            Status: 0
        },
        {
            MarketId: 'th-TH',
            MarketName: 'Thailand',
            Status: 1
        },
        {
            MarketId: 'zh-CN',
            MarketName: 'Chinese',
            Status: 2
        }
    ];

    const defaultProps = {
        selectedDomainId: 123,
        isAffDomain: true,
        getMarketListByDomain: jest.fn(() => marketListByDomains),
        getDomainById: jest.fn(() => selectedDomain),
        toggleLoader: jest.fn(),
        fetchData: jest.fn(),
        setSelectedDomainId: jest.fn(),
        insertDomain: jest.fn(),
        updateDomain: jest.fn()
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    describe('Rendering', () => {
        it('should render correctly for insert mode', () => {
            const props = { ...defaultProps, selectedDomainId: 0 };
            const wrapper = shallow(<DomainPopup {...props} />);
            wrapper.setState({ selectedDomain: emptyDomain });
            expect(wrapper).toMatchSnapshot();
        });

        it('should render correctly for update mode', () => {
            const wrapper = shallow(<DomainPopup {...defaultProps} />);
            wrapper.setState({ 
                isSuccess: false, 
                selectedDomain, 
                marketListByDomains, 
                usingMarkets: ["en-US"] 
            });
            expect(wrapper).toMatchSnapshot();
        });

        it('should not render popup when selectedDomainId is null', () => {
            const props = { ...defaultProps, selectedDomainId: null };
            const wrapper = shallow(<DomainPopup {...props} />);
            expect(wrapper).toMatchSnapshot();
        });
    });

    describe('Lifecycle Methods', () => {
        it('should call _handleUpdateState when component updates with new selectedDomainId', () => {
            const props = { ...defaultProps, selectedDomainId: 234 };
            const wrapper = shallow(<DomainPopup {...props} />);
            const instance = wrapper.instance();
            instance._handleUpdateState = jest.fn();

            instance.componentDidUpdate(defaultProps);

            expect(instance._handleUpdateState).toHaveBeenCalledWith(234, true);
        });
    });

    describe('State Management', () => {
        describe('_handleUpdateState', () => {
            it('should handle insert mode correctly', async () => {
                const marketListByDomain = [{ MarketId: 'en-US', MarketName: "English" }];
                const props = {
                    ...defaultProps,
                    getMarketListByDomain: jest.fn(() => marketListByDomain),
                    toggleLoader: jest.fn()
                };
                const wrapper = shallow(<DomainPopup {...props} />);
                const instance = wrapper.instance();
                instance.setState = jest.fn();

                await instance._handleUpdateState(0, false);

                expect(props.getMarketListByDomain).toHaveBeenCalledWith({ affDomainId: 0, redirectDomainId: 0 });
                expect(instance.setState).toHaveBeenCalledWith({
                    isInsert: true,
                    marketListByDomains: [{ MarketId: "en-US", MarketName: "English" }],
                    selectedDomain: { ...emptyDomain }
                });
            });

            it('should handle update mode correctly', async () => {
                const domain = { DomainId: 123, DomainName: "abc.com", IsSSL: true };
                const marketListByDomain = [
                    { MarketId: 'en-US', MarketName: "English", Status: 0 },
                    { MarketId: 'th-TH', MarketName: "Thailand", Status: 2 }
                ];
                const props = {
                    ...defaultProps,
                    getMarketListByDomain: jest.fn(() => marketListByDomain),
                    getDomainById: jest.fn(() => domain),
                    toggleLoader: jest.fn()
                };
                const wrapper = shallow(<DomainPopup {...props} />);
                const instance = wrapper.instance();
                instance.setState = jest.fn();

                await instance._handleUpdateState(123, true);

                expect(props.toggleLoader).toHaveBeenCalledTimes(2);
                expect(props.getDomainById).toHaveBeenCalledWith(123, true);
                expect(props.getMarketListByDomain).toHaveBeenCalledWith({ affDomainId: 123 });
                expect(instance.setState).toHaveBeenCalledWith({
                    isBlockAllMarkets: false,
                    isInsert: false,
                    marketListByDomains: [
                        {
                            MarketId: 'en-US',
                            MarketName: 'English',
                            Status: 0
                        },
                        {
                            MarketId: 'th-TH',
                            MarketName: 'Thailand',
                            Status: 2
                        }
                    ],
                    selectedDomain: {
                        blockMarkets: [],
                        domainId: 123,
                        domainName: "abc.com",
                        isSSL: true
                    },
                    usingMarkets: ["th-TH"]
                });
            });
        });

        describe('Market Management', () => {
            it('should handle market checkbox changes correctly', () => {
                const event = {
                    target: {
                        type: 'checkbox',
                        checked: true
                    }
                };
                const wrapper = shallow(<DomainPopup {...defaultProps} />);
                wrapper.setState({ selectedDomain, marketListByDomains });
                const instance = wrapper.instance();
                instance.setState = jest.fn();

                instance._handleChangeMarket(event, "th-TH");

                expect(instance.setState).toHaveBeenCalledWith({
                    isBlockAllMarkets: false,
                    marketListByDomains: marketListByDomains.map(market => 
                        market.MarketId === "th-TH" 
                            ? { ...market, Status: 1 }
                            : market
                    ),
                    selectedDomain: {
                        ...selectedDomain,
                        blockMarkets: ["th-TH"]
                    }
                });
            });

            it('should handle "block all markets" checkbox correctly', () => {
                const event = {
                    target: {
                        type: 'checkbox',
                        checked: true
                    }
                };
                const wrapper = shallow(<DomainPopup {...defaultProps} />);
                wrapper.setState({ selectedDomain, marketListByDomains });
                const instance = wrapper.instance();
                instance.setState = jest.fn();

                instance._handleChangeAllMarkets(event);

                expect(instance.setState).toHaveBeenCalledWith({
                    isBlockAllMarkets: true,
                    marketListByDomains: marketListByDomains.map(market => ({
                        ...market,
                        Status: market.Status != 2 ? 1 : market.Status
                    })),
                    selectedDomain: {
                        ...selectedDomain,
                        blockMarkets: marketListByDomains.filter(market => market.Status != 2).map(market => market.MarketId)
                    }
                });
            });

            it('should handle unchecking "block all markets" checkbox correctly', () => {
                const event = {
                    target: {
                        type: 'checkbox',
                        checked: false
                    }
                };
                const wrapper = shallow(<DomainPopup {...defaultProps} />);
                wrapper.setState({ 
                    selectedDomain: {...selectedDomain, blockMarkets: ["en-US", "th-TH"]},
                    marketListByDomains,
                    isBlockAllMarkets: true
                });
                const instance = wrapper.instance();
                instance.setState = jest.fn();

                instance._handleChangeAllMarkets(event);

                expect(instance.setState).toHaveBeenCalledWith({
                    isBlockAllMarkets: false,
                    marketListByDomains: marketListByDomains.map(market => ({
                        ...market,
                        Status: market.Status != 2 ? 0 : market.Status
                    })),
                    selectedDomain: {
                        ...selectedDomain,
                        blockMarkets: []
                    }
                });
            });
        });

        describe('_handleChange', () => {
            it('should update domain name correctly', () => {
                const event = {
                    target: {
                        type: 'text',
                        name: 'domainName',
                        value: '123'
                    }
                };
                const wrapper = shallow(<DomainPopup {...defaultProps} />);
                wrapper.setState({ selectedDomain });
                wrapper.setState = jest.fn();

                wrapper.find('Input[type="text"][name="domainName"]').simulate('change', event);

                expect(wrapper.setState).toHaveBeenCalledWith({
                    selectedDomain: {
                        ...selectedDomain,
                        domainName: "123"
                    }
                });
            });

            it('should update SSL status correctly', () => {
                const event = {
                    target: {
                        type: 'checkbox',
                        name: 'isSSL',
                        checked: false
                    }
                };
                const wrapper = shallow(<DomainPopup {...defaultProps} />);
                wrapper.setState({ selectedDomain });
                wrapper.setState = jest.fn();

                wrapper.find('input[type="checkbox"][name="isSSL"]').simulate('change', event);

                expect(wrapper.setState).toHaveBeenCalledWith({
                    selectedDomain: {
                        ...selectedDomain,
                        isSSL: false
                    }
                });
            });
        });
    });

    describe('Form Submission', () => {
        it('should handle successful domain insertion', async () => {
            const result = { IsSuccessful: true, Message: "" };
            const props = {
                ...defaultProps,
                insertDomain: jest.fn(() => result),
                toggleLoader: jest.fn()
            };
            const wrapper = shallow(<DomainPopup {...props} />);
            const instance = wrapper.instance();
            instance.setState({ selectedDomain });
            instance._handleShowAlert = jest.fn();

            await instance._handleSubmit();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Add Successfully!");
        });

        it('should handle successful domain update', async () => {
            const result = { IsSuccessful: true, Message: "" };
            const props = {
                ...defaultProps,
                updateDomain: jest.fn(() => result),
                toggleLoader: jest.fn()
            };
            const wrapper = shallow(<DomainPopup {...props} />);
            wrapper.setState({ isInsert: false });
            const instance = wrapper.instance();
            instance.setState({ selectedDomain });
            instance._handleShowAlert = jest.fn();

            await instance._handleSubmit();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(instance._handleShowAlert).toHaveBeenCalledWith(true, "Update Successfully!");
        });

        it('should handle failed domain operation', async () => {
            const result = { IsSuccessful: false, Message: "Operation failed" };
            const props = {
                ...defaultProps,
                insertDomain: jest.fn(() => result),
                toggleLoader: jest.fn()
            };
            const wrapper = shallow(<DomainPopup {...props} />);
            const instance = wrapper.instance();
            instance.setState({ selectedDomain });
            instance._handleShowAlert = jest.fn();

            await instance._handleSubmit();

            expect(props.toggleLoader).toHaveBeenCalledTimes(2);
            expect(instance._handleShowAlert).toHaveBeenCalledWith(false, "Operation failed");
        });
    });

    describe('Alert Handling', () => {
        it('should show and hide alert correctly', () => {
            const wrapper = shallow(<DomainPopup {...defaultProps} />);
            wrapper.setState = jest.fn((obj, fn) => fn && fn());
            const instance = wrapper.instance();
            instance._hideMessage = jest.fn();

            instance._handleShowAlert(false, 'Operation failed!');

            expect(wrapper.setState).toHaveBeenCalledWith({
                isShowMessage: true,
                isSuccess: false,
                message: 'Operation failed!'
            }, expect.any(Function));
        });

        it('should handle alert hide with success', () => {
            const props = { ...defaultProps, fetchData: jest.fn() };
            const wrapper = shallow(<DomainPopup {...props} />);
            wrapper.setState = jest.fn();
            const instance = wrapper.instance();
            instance._handleClosePopup = jest.fn();

            instance._hideMessage(true);

            expect(wrapper.setState).toHaveBeenCalledWith({ isShowMessage: false });
            expect(props.fetchData).toHaveBeenCalledWith(true);
            expect(instance._handleClosePopup).toHaveBeenCalled();
        });
    });

    describe('Keyboard Events', () => {
        it('should handle enter key press in domain name input', () => {
            const event = {
                charCode: 13,
                preventDefault: jest.fn()
            };
            const wrapper = shallow(<DomainPopup {...defaultProps} />);
            wrapper.setState({ selectedDomain });
            wrapper.instance()._handleSubmit = jest.fn();

            wrapper.find('Input[type="text"]').simulate('keyPress', event);

            expect(event.preventDefault).toHaveBeenCalled();
            expect(wrapper.instance()._handleSubmit).toHaveBeenCalled();
        });

        it('should ignore non-enter key presses', () => {
            const event = {
                charCode: 16,
                preventDefault: jest.fn()
            };
            const wrapper = shallow(<DomainPopup {...defaultProps} />);
            wrapper.setState({ selectedDomain });

            wrapper.find('Input[type="text"]').simulate('keyPress', event);

            expect(event.preventDefault).not.toHaveBeenCalled();
        });
    });

    describe('Popup Management', () => {
        it('should close popup correctly', () => {
            const props = { ...defaultProps, setSelectedDomainId: jest.fn() };
            const wrapper = shallow(<DomainPopup {...props} />);

            wrapper.instance()._handleClosePopup();

            expect(props.setSelectedDomainId).toHaveBeenCalledWith(null);
        });
    });
});