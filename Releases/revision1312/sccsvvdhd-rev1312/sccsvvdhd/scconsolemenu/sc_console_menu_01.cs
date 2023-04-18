using System;
namespace sccs.sc_console_menu
{
    public static class sc_console_menu_01
    {
        static sccs.scmessageobject.scmessageobject _someReceivedObject0 = new sccs.scmessageobject.scmessageobject();
        static sccs.scmessageobject.scmessageobject _data00_IN;
        static sccs.scmessageobject.scmessageobject _toReturnObject;

        public static sccs.scmessageobject.scmessageobject _console_menu(object _main_object)
        {
            try
            {
                _data00_IN = (sccs.scmessageobject.scmessageobject)_main_object;

                int _received_switch_in00 = _data00_IN._received_switch_in;   
                int _received_switch_out00 = _data00_IN._received_switch_out; 
                int _sending_switch_in00 = _data00_IN._sending_switch_in;     
                int _sending_switch_out00 = _data00_IN._sending_switch_out;   
                int _timeOut00 = _data00_IN._timeOut0;
                int _ParentTaskThreadID00 = _data00_IN._ParentTaskThreadID0;
                int _main_cpu_count00 = _data00_IN._main_cpu_count;
                string _passTest00 = _data00_IN._passTest;
                int _welcomePackage00 = _data00_IN._welcomePackage;
                int _current_menu00 = _data00_IN._current_menu;
                int _last_current_menu00 = _data00_IN._last_current_menu;
                string _menuOption = _data00_IN._menuOption;

                _someReceivedObject0 = _data00_IN;

                if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 0 &&
                    _sending_switch_in00 == 0 &&
                    _sending_switch_out00 == 0)
                {
                    if (_menuOption.ToLower() == "vr" ||
                        _menuOption.ToLower() == "std" ||
                        _menuOption.ToLower() == "standard" ||
                        _menuOption.ToLower() == "command" ||
                        _menuOption.ToLower() == "commands" ||
                        _menuOption.ToLower() == "credit" ||
                        _menuOption.ToLower() == "credits" ||
                        _menuOption.ToLower() == "singleplayer" ||
                        _menuOption.ToLower() == "multiplayer" ||
                        _menuOption.ToLower() == "single" ||
                        _menuOption.ToLower() == "multi")
                    {
                        if (_menuOption.ToLower() == "vr")
                        {
                            _someReceivedObject0._current_menu = 0;

                            _toReturnObject = _someReceivedObject0;
                            return _toReturnObject;
                        }
                        else if (_menuOption.ToLower() == "standard" || _menuOption.ToLower() == "std")
                        {
                            _someReceivedObject0._current_menu = 1;
                            _toReturnObject = _someReceivedObject0;
                            return _toReturnObject;
                        }
                    }
                    else
                    {
                        _someReceivedObject0._received_switch_in = 0;
                        _someReceivedObject0._received_switch_out = 0;
                        _someReceivedObject0._sending_switch_in = 0;
                        _someReceivedObject0._sending_switch_out = 0;
                        _someReceivedObject0._welcomePackage = 1;
                        _someReceivedObject0._work_done = 0;
                        _someReceivedObject0._current_menu = -1;
                    }
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 0 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                         _received_switch_out00 == 0 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;

                }
                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 0)
                {

                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                     _received_switch_out00 == 0 &&
                     _sending_switch_in00 == 1 &&
                     _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                       _received_switch_out00 == 0 &&
                       _sending_switch_in00 == 0 &&
                       _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                    _received_switch_out00 == 1 &&
                    _sending_switch_in00 == 1 &&
                    _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 0 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                      _received_switch_out00 == 0 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                           _received_switch_out00 == 0 &&
                           _sending_switch_in00 == 1 &&
                           _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 0 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 1 &&
                        _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }

                else if (_received_switch_in00 == 1 &&
                        _received_switch_out00 == 1 &&
                        _sending_switch_in00 == 0 &&
                        _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                         _received_switch_out00 == 1 &&
                         _sending_switch_in00 == 1 &&
                         _sending_switch_out00 == 0)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }
                else if (_received_switch_in00 == 1 &&
                      _received_switch_out00 == 1 &&
                      _sending_switch_in00 == 1 &&
                      _sending_switch_out00 == 1)
                {
                    _someReceivedObject0._received_switch_in = 0;
                    _someReceivedObject0._received_switch_out = 0;
                    _someReceivedObject0._sending_switch_in = 0;
                    _someReceivedObject0._sending_switch_out = 0;
                    _toReturnObject = _someReceivedObject0;
                    return _toReturnObject;
                }

            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "Fail 00" + ex.Message, "Oculus error", 0);
            }
            return _toReturnObject;
        }
    }
}
