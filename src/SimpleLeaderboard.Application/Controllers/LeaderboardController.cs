using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleLeaderboard.Domain;
using SimpleLeaderboard.Infrastructure;

namespace SimpleLeaderboard.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILogger<LeaderboardController> _logger;
        private readonly ISimpleLeaderboardRead _read;
        private readonly ISimpleLeaderboardWrite _write;

        public LeaderboardController(ILogger<LeaderboardController> logger, ISimpleLeaderboardRead read, ISimpleLeaderboardWrite write)
        {
            _logger = logger;
            _read = read ?? throw new ArgumentException(nameof(ISimpleLeaderboardRead));
            _write = write ?? throw new ArgumentException(nameof(ISimpleLeaderboardWrite));
        }

        [HttpGet("event/")]
        public async Task<ActionResult<IEnumerable<LeaderboardEventDto>>> GetEvent(string eventGuid) 
        {
            try 
            {
                var events = await _read.GetLeaderboardEvents(w => string.IsNullOrEmpty(eventGuid) || w.UniqueId == eventGuid || w.AdminId == eventGuid);
                return Ok(events);
            } catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return NotFound();
            }
        }

        [HttpGet("leaderboards/id/{eventId:int}")]
        public async Task<ActionResult<LeaderboardDto>> GetLeaderboards(int eventId) {
            try 
            {
                var leaderboard = await _read.GetLeaderboards(w => w.LeaderboardEvent.LeaderboardEventId == eventId);
                return Ok(leaderboard);
            } catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return NotFound();
            }
        }

        [HttpGet("leaderboards/{eventId}")]
        public async Task<ActionResult<IEnumerable<LeaderboardDto>>> GetLeaderboards(string eventId) {
            try 
            {
                var leaderboard = await _read.GetLeaderboards(w => w.LeaderboardEvent.UniqueId == eventId || w.LeaderboardEvent.AdminId == eventId);
                return Ok(leaderboard);
            } catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return NotFound();
            }
        }

        [HttpGet("leaderboard/{guid}")]
        public async Task<ActionResult<LeaderboardDto>> GetLeaderboard(string guid) 
        {
            try 
            {
                var leaderboard = (await _read.GetLeaderboards(w => w.UniqueId == guid || w.AdminId == guid)).FirstOrDefault() ?? throw new ArgumentException("invalid id");
                return Ok(leaderboard);
            } catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return NotFound();
            }
        }

        [HttpPost("event")]
        public async Task<ActionResult<LeaderboardEventDto>> CreateEvent([FromBody] CreateEventDto eventToCreate) {
            try 
            {
                var newEvent = await _write.CreateEvent(eventToCreate.Title, eventToCreate.Description, eventToCreate.IsDescending, eventToCreate.UniqueId, eventToCreate.AdminId);
                return Ok(newEvent);
            }catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest();
            }
        }

        [HttpPost("leaderboard")]
        public async Task<ActionResult<LeaderboardDto>> CreateLeaderboard([FromBody] CreateLeaderboardDto leaderboard)
        {
            try 
            {
                var players = GetPlayersFromCreate(leaderboard.Players);
                var updatedEvent = await _write.AddLeaderBoard(leaderboard.LeaderboardEventId, leaderboard.Title, players,leaderboard.UniqueId);
                return Ok(updatedEvent);
            }catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest();
            }
        }
        [HttpPost("player")]
        public async Task<ActionResult<IEnumerable<LeaderboardPlayerDto>>> AddPlayer([FromBody] CreatePlayerDto createPlayers)
        {
            try 
            {
                var res = await _write.AddPlayer(createPlayers.LeaderboardId, GetPlayersFromCreate(createPlayers.Players));
                var players = res.Select(s => LeaderboardPlayerDto.Create(s));
                return Ok(players);
            }catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest();
            }
        }

        [HttpPut("player")]
        public async Task<ActionResult<LeaderboardPlayer>> UpdatePlayerScore([Required]int leaderboardId, [Required] int playerId, double playerScore)
        {
            try 
            {
                var newEvent = await _write.UpdatePlayerScore(leaderboardId, playerId, playerScore);
                return Ok(newEvent);
            }catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest();
            }
        }

        [HttpPut("event")]
        public async Task<ActionResult<LeaderboardEvent>> EndEvent([Required]int leaderboardEventId)
        {
            try 
            {
                var newEvent = await _write.EndEvent(leaderboardEventId);
                return Ok(newEvent);
            }catch (Exception e) {
                _logger.Log(LogLevel.Error, e.Message);
                return BadRequest();
            }
        }


        private IEnumerable<LeaderboardPlayer> GetPlayersFromCreate(IEnumerable<NewPlayerDto> newPlayers)
        {
            return newPlayers.Select(s => new LeaderboardPlayer {
                    Name = s.Name,
                    Nickname = s.Nickname
                });
        }
    }
}
