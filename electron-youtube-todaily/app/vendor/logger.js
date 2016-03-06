var winston = require('winston');
var fs = require('fs');
function log(){

}
log.prototype.createLogger=function(filenamePath) {
  // Set up logger
  // 
  var customColors = {
    trace: 'white',
    debug: 'green',
    info: 'green',
    warn: 'yellow',
    crit: 'red',
    fatal: 'red'
  };

  var logger = new(winston.Logger)({
    colors: customColors,
    levels: {
      trace: 0,
      debug: 1,
      info: 2,
      warn: 3,
      crit: 4,
      fatal: 5
    },
    transports: [
      new(winston.transports.File)({
        name: 'fileLogger',
        level: 'info',
        filename: filenamePath,
        maxsize: 104857600 // 100 mb
      })
    ]
  });

  winston.addColors(customColors);

  // Extend logger object to properly log 'Error' types
  var origLog = logger.log;

  logger.log = function(level, msg) {
    if (msg instanceof Error) {
      var args = Array.prototype.slice.call(arguments);
      args[1] = msg.stack;
      origLog.apply(logger, args);
    } else {
      origLog.apply(logger, arguments);
    }
  };

  
  /* LOGGER EXAMPLES
      app.logger.trace('testing');
      app.logger.debug('testing');
      app.logger.info('testing');
      app.logger.warn('testing');
      app.logger.crit('testing');
      app.logger.fatal('testing');
      */
     return logger;
};

module.exports = new log();