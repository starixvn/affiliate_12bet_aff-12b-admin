describe("Layout", function () {
    describe('initialize', function () {
        it('initial functions should be called', function () {
            spyOn(Layout, 'registerAjaxHandlerError');
            spyOn(Layout, 'initEvent');
            spyOn(Layout, 'renderHotNews');

            Layout.initialize();

            expect(Layout.registerAjaxHandlerError).toHaveBeenCalled();
            expect(Layout.initEvent).toHaveBeenCalled();
            expect(Layout.renderHotNews).toHaveBeenCalled();
        });
    })
});